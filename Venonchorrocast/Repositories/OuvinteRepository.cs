using System.IO;
using System.Web;
using Venonchorrocast.Models;

namespace Venonchorrocast.Repositories
{
    public class OuvinteRepository
    {
        private readonly ConexaoBancoDeDados db = new ConexaoBancoDeDados();
        public int UploadImageInDataBase(HttpPostedFileBase file, Ouvinte ouvinte)
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/avatarvazio.jpg");
            byte[] fotinha = File.ReadAllBytes(path);

            ouvinte.Foto = ConvertToBytes(file);
            var Ouvinte = new Ouvinte
            {
                id_ouvinte = ouvinte.id_ouvinte,
                Nome = ouvinte.Nome,
                Email = ouvinte.Email,
                Senha = ouvinte.Senha,
                Foto = ouvinte.Foto
            };
            if(Ouvinte.Foto.Length==0)
            {
                ouvinte.Foto = fotinha;
            }
            db.Ouvinte.Add(Ouvinte);
            int i = db.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

    }
}