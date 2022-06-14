using Dapper;
using DapperDenemeleri.Application.Interfaces;
using DapperDenemeleri.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDenemeleri.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;
        private readonly IDbTransaction _dbTransaction;

        public ProductRepository(IConfiguration configuration, SqlConnection connection, IDbTransaction dbTransaction)
        {
            _configuration = configuration;
            _connection = connection;
            _dbTransaction = dbTransaction;
        }

        public async Task<int> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            ////_connection.Open();
            var result =await _connection.ExecuteAsync(sql, entity,transaction:_dbTransaction);
            return result;
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
            //    connection.Open();
            //    var result = await connection.ExecuteAsync(sql, entity);
            //    return result;
            //}
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
                //_connection.Open();
                var result = await _connection.ExecuteAsync(sql, new { Id = id }, transaction: _dbTransaction);
                return result;
            //}
        }
        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
                //_connection.Open();
                var result = await _connection.QueryAsync<Product>(sql, transaction: _dbTransaction);
                return result.ToList();
            //}
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
                //_connection.Open();
                var result = await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id }, transaction: _dbTransaction);
                return result;
            //}
        }
        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            //using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            //{
                //_connection.Open();
                var result = await _connection.ExecuteAsync(sql, entity, transaction: _dbTransaction);
                return result;
           // }
        }
    }
}
