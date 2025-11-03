using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace Colegio.Utilidades
{
    public class Seguridad
    {

        private static readonly string ClaveSecreta = "MyApp@2025_SecretKey#123!"; 
        public static string EncriptarContraseina(string contraseina, string nombreUsuario)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] salt = sha256.ComputeHash(System.Text.Encoding.ASCII.GetBytes(nombreUsuario + ClaveSecreta));

                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: contraseina,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                ));

                return hashedPassword;
            }
        }

    }

}
