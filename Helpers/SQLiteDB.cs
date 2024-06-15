using SQLite;


namespace Lerdo_MX_PQM.SQLite
{ 

    public class SQLiteDB
    {

        private const string DbName = "PQM.db3";

        private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Database =>
            (_connection ??= new SQLiteAsyncConnection(DbPath,
                SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));
       
        public SQLiteDB()
        {
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

        public async Task InsertRangeItem<T>(object Lista)
        {
            await Database.InsertAllAsync(Lista as List<T>);
        }

        public async Task InsertItem(object item)
        {
            await Database.InsertAsync(item);
        }

        public async void DropTable<Table>() where Table : class, new()
        {
            await Database.DropTableAsync<Table>();
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

