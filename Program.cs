using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace RepoMalo
{
    class Program
    {
        // 1. SECRETOS EXPUESTOS: Credenciales quemadas en el código (Hardcoded Secrets)
        private const string AWS_ACCESS_KEY_ID = "AKIA5X7B9M2P4Q8R1T3V";
        private const string AWS_SECRET_ACCESS_KEY = "wJalrXUtnFEMI/K7MDENG/bPxRfiCYzEXAMPLEKEY";
        private const string SQL_CONN = "Server=10.0.0.50;Database=BD_Produccion;User Id=sa;Password=ProdSuperSecret2026!;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            string usuarioInput = args.Length > 0 ? args[0] : "admin' OR '1'='1";
            string archivoInput = args.Length > 1 ? args[1] : "../../Windows/win.ini";
            string ipInput = args.Length > 2 ? args[2] : "8.8.8.8 & whoami";

            // Variable sin usar (Code Smell para SonarQube)
            int variableBasuraSinUso = 999;

            // 2. VULNERABILIDAD SAST (CWE-89): Inyección SQL por concatenación directa
            using (SqlConnection conn = new SqlConnection(SQL_CONN))
            {
                string query = "SELECT * FROM Usuarios WHERE Usuario = '" + usuarioInput + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                Console.WriteLine("Ejecutando consulta insegura: " + query);
            }

            // 3. VULNERABILIDAD SAST (CWE-78): Inyección de comandos en el sistema operativo
            Process.Start("cmd.exe", "/c ping " + ipInput);

            // 4. VULNERABILIDAD SAST (CWE-22): Path Traversal (lectura arbitraria de archivos)
            string contenido = File.ReadAllText("C:\\Reportes\\" + archivoInput);

            // 5. VULNERABILIDAD SAST (CWE-328): Uso de algoritmo criptográfico débil (MD5)
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes("ClaveSecreta123"));
            }

            // 6. VULNERABILIDAD SAST (CWE-295): Deshabilitar validación de certificados SSL
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            // 7. MALA PRÁCTICA (Calidad): Bloque catch vacío que silencia errores críticos
            try
            {
                int divisor = 0;
                int resultado = 10 / divisor;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
