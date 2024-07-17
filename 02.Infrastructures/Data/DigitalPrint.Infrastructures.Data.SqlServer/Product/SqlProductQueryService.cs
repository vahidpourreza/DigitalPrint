using Dapper;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.DTOs;
using DigitalPrint.Core.Domain.Products.Queries;
using Microsoft.Data.SqlClient;

namespace DigitalPrint.Infrastructures.Data.SqlServer.Product;

public class SqlProductQueryService : IProductQueryService
{
    private readonly SqlConnection _sqlConnection;

    public SqlProductQueryService(SqlConnection sqlConnection)
    {
       _sqlConnection = sqlConnection;
    }

    public ProductDetail Query(GetActiveProduct query)
    {
        string sqlQuery = @"
            SELECT TOP 1 
                p.Id AS ProductId,
                p.Title,
                p.Price,
                p.Description,
                up.DisplayName AS CreatorDisplayName,
                STRING_AGG(pic.Location, ',') AS PhotoUrls
            FROM 
                Products p
                INNER JOIN UserProfiles up ON p.CreatorId = up.Id
                LEFT JOIN Picture pic ON p.Id = pic.ProductId
            WHERE 
                p.Id = @ProductId AND
                p.IsDeleted = 0 AND
                p.Status = 3
            GROUP BY 
                p.Id, p.Title, p.Price, p.Description, up.DisplayName
            ORDER BY 
                MIN(pic.[Order]);
        ";

        return _sqlConnection.QuerySingleOrDefault<ProductDetail>(sqlQuery, new { query.ProductId });
    }
    public IEnumerable<ProductSummary> Query(GetActiveProductList query)
    {
        string sqlQuery = @"
                           SELECT 
                               p.Id AS ProductId,
                               p.Title,
                               p.Price,
                               MIN(pic.Location) AS PhotoUrl
                           FROM 
                               products p
                               LEFT JOIN picture pic ON p.Id = pic.ProductId
                           WHERE 
                               p.IsDeleted = 0 AND
                               p.Status = 3
                           GROUP BY 
                               p.Id, p.Title, p.Price, p.CreationDate
                           ORDER BY 
                               p.CreationDate
                           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                       ";


        return _sqlConnection.Query<ProductSummary>(sqlQuery, new
        {
            Offset = (query.PageNumber - 1) * query.PageSize,
            PageSize = query.PageSize
        });
    }
    public IEnumerable<ProductSummary> Query(GetProductForSpecificCreator query)
    {
        string sqlQuery = @"
                         SELECT 
                             p.Id AS ProductId,
                             p.Title,
                             p.Price,
                             MIN(pic.Location) AS PhotoUrl
                         FROM 
                             products p
                             LEFT JOIN picture pic ON p.Id = pic.ProductId
                         WHERE 
                             p.CreatorId = @CreatorUserId AND
                             p.IsDeleted = 0 AND
                             p.Status = 3
                         GROUP BY 
                             p.Id, p.Title, p.Price
                         ORDER BY 
                             p.Id -- Use a stable column for ordering if CreationDate cannot be used directly
                         OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                     ";

        return _sqlConnection.Query<ProductSummary>(sqlQuery, new
        {
            CreatorUserId = query.CreatorUserId,
            Offset = (query.PageNumber - 1) * query.PageSize,
            PageSize = query.PageSize
        });
    }

}
