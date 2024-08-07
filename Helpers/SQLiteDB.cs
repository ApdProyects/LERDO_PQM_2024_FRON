﻿using SQLite;


namespace Lerdo_MX_PQM.SQLite
{ 

    public class SQLiteDB
    {

        private const string DbName = "PQM.db3";

        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Database { get { return _connection; } }
       
        public SQLiteDB()
        {
            _connection ??= new SQLiteAsyncConnection(DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);


            /*inicializamos las tablas en SQLite*/
            CreateTables<clsInspector>();
            CreateTables<clsMarcas>();
            CreateTables<clsLineas>();
            CreateTables<clsColores>();
            CreateTables<clsGarantias>();
            CreateTables<clsEstados>();
            CreateTables<clsLugares>();
            CreateTables<clsMotivos>();
            /*nuevos metodos no existentes*/
            CreateTables<clsProcedencia>();
            CreateTables<MontoInfraccion>();
            CreateTables<ClsImpresoras>();
            CreateTables<ClsEstructuratiket>();

            CreateTables<AppConfig>(); /*configuracion inicial*/
        }

        public async Task CreateTables<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }
        public async Task<int> InsertRangeItem<T>(object Lista)
        {
            return await Database.InsertAllAsync(Lista as List<T>);
        }

        public async Task InsertItem(object item)
        {
            await Database.InsertAsync(item);
        }

        public async void DropTable<Table>() where Table : class, new()
        {
            await Database.DropTableAsync<Table>();
        }

        public async Task<int> DeleteTable<Table>() where Table : class, new()
        {
            return await Database.DeleteAllAsync<Table>();
        }

        public async Task<int> DeleteRow(object obj)
        {
            return await Database.DeleteAsync(obj);
        }

        public async Task<List<Table>> GetItemsTable<Table>() where Table : class, new()
        {
            var table = Database.Table<Table>();
            return await table.ToListAsync();
        }
        
        // Método para actualizar una sola fila
        public async Task<int> UpdateItemAsync<T>(T item) where T : class, new()
        {
            return await Database.UpdateAsync(item);
        }
    }
}

