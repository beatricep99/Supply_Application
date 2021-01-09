using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Supply_Application.Models;

namespace Supply_Application.Data
{
    public class SupplyListDatabase
    {
        readonly SQLiteAsyncConnection _database;


        //constructorul ia ca si argument CALEA CATRE FISIRUL de tip BD
        public SupplyListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SupplyList>().Wait();

            _database.CreateTableAsync<Product>().Wait();
            _database.CreateTableAsync<ListProduct>().Wait();
        }


        public Task<List<SupplyList>> GetSupplyListsAsync()
        {
            return _database.Table<SupplyList>().ToListAsync();
        }


        public Task<SupplyList> GetSupplyListAsync(int id)
        {
            return _database.Table<SupplyList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }


        public Task<int> SaveSupplyListAsync(SupplyList slist)
        {
            if (slist.ID != -1)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }


        public Task<int> DeleteSupplyListAsync(SupplyList slist)
        {
            return _database.DeleteAsync(slist);
        }


        public Task<int> SaveProductAsync(Product product)
        {
            if (product.ID != 0)
            {
                return _database.UpdateAsync(product);
            }
            else
            {
                return _database.InsertAsync(product);
            }
        }


        public Task<int> DeleteProductAsync(Product product)
        {
            return _database.DeleteAsync(product);
        }


        public Task<List<Product>> GetProductsAsync()
        {
            return _database.Table<Product>().ToListAsync();
        }


        public Task<int> SaveListProductAsync(ListProduct listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }

        public Task<int> DeleteListProductAsync(ListProduct listProduct)
        {
            return _database.DeleteAsync(listProduct);
        }

        public Task<List<ListProduct>> GetListProductAsyncTest(int supplyListID)
        {
            return _database.Table<ListProduct>()
            .Where(i => i.SupplyListID == supplyListID).ToListAsync();
        }

        public Task<List<Product>> GetListProductsAsync(int supplyList)
        {
            return _database.QueryAsync<Product>(
            "select P.ID, P.Description from Product P"
            + " inner join ListProduct LP"
            + " on P.ID = LP.ProductID where LP.SupplyListID = ?", supplyList);
        }

    }
}
    
