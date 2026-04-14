using AgendaWeb.Data.Entities;
using Microsoft.Data.SqlClient;

namespace AgendaWeb.Data.Commands
{
    public class ContactoCommand
    {
        private readonly SQLServer _sqlServer;

        public ContactoCommand(SQLServer sqlServer)
        {
            _sqlServer = sqlServer;
        }

        public async Task<int> InsertarContactoAsync(Contacto contacto)
        {
            string query = "INSERT INTO Contactos" +
                " (Nombre, Telefono, Email) " +
                "VALUES " +
                "(@Nombre, @Telefono, @Email)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nombre", contacto.Nombre),
                new SqlParameter("@Telefono", contacto.Telefono),
                new SqlParameter("@Email", contacto.Email)
            };
            return await _sqlServer.NonQueryAsync(query, parameters);
        }

        public async Task<int> EliminarContactoAsync(int id)
        {
            string query = "DELETE FROM Contactos WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };
            return await _sqlServer.NonQueryAsync(query, parameters);
        }

        public async Task<int> ActualizarContactoAsync(int id, Contacto contacto)
        {
            string query = "UPDATE Contactos " +
                "SET" +
                " Nombre = @Nombre, " +
                "Telefono = @Telefono, " +
                "Email = @Email " +
                "WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Nombre", contacto.Nombre),
                new SqlParameter("@Telefono", contacto.Telefono),
                new SqlParameter("@Email", contacto.Email)
            };
            return await _sqlServer.NonQueryAsync(query, parameters);
        }

        public async Task<List<Contacto>> ObtenerTodosAsync()
        {
            string query = "SELECT Id, Nombre, Telefono, Email " +
                "FROM Contactos " +
                //"{ WHERE Nombre LIKE '%gael%'} " +
                "ORDER BY Nombre";
            List<Contacto> contactos = new List<Contacto>();
            contactos = await _sqlServer.ReaderListAsync<Contacto>(query);
            return contactos;
        }
    }
}
