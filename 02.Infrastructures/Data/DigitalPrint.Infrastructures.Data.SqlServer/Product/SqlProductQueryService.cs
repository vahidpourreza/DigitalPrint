using Dapper;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.Products.DTOs;
using DigitalPrint.Core.Domain.Products.Queries;
using Microsoft.Data.SqlClient;

namespace DigitalPrint.Infrastructures.Data.SqlServer.Product;

public class SqlProductQueryService : IProductQueryService
{
    private readonly SqlConnection sqlConnection;

    public SqlProductQueryService(SqlConnection sqlConnection)
    {
        this.sqlConnection = sqlConnection;
    }

    public ProductDetail Query(GetActiveProduct query)
    {
        string sqlQuery = "Select Top 1 a.Id as 'ProductId'," +
                          " a.Title,a.Text,p.Location as 'photoUrls', up.DisplayName as 'SellersDisplayName' " +
                          " FROM Advertisments a " +
                          " Inner Join Picture p on a.Id = p.AdvertismentId " +
                          " Inner Join UserProfiles up on a.OwnerId = up.Id" +
                          " Where State = 2 and " +
                          " a.Id = @ProductId " +
                          " Order By p.[Order]";
        return sqlConnection.QuerySingleOrDefault<ProductDetail>(sqlQuery, new { query.ProductId });
    }

    public ProductSummary Query(GetActiveProductList query)
    {
        throw new NotImplementedException();
    }

    public ProductSummary Query(GetProductForSpecificCreator query)
    {
        throw new NotImplementedException();
    }
}
