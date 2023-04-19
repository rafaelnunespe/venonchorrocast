using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Venonchorrocast.Models;

namespace Venonchorrocast.Repositories
{
    public class EpisodioRepository
    {
        private readonly ConexaoBancoDeDados db = new ConexaoBancoDeDados();
        public int UploadImageInDataBase(HttpPostedFileBase file, Episodio episodio)
        {
            episodio.Arte = ConvertToBytes(file);
            var Episodio = new Episodio
            {
                id_episodio = episodio.id_episodio,
                Titulo = episodio.Titulo,
                Descriçao = episodio.Descriçao,
                Duraçao = episodio.Duraçao,
                Link = episodio.Link,
                Arte = episodio.Arte,
                Data_Public = System.DateTime.Now,
                Participantes = episodio.Participantes
            };
            db.Episodio.Add(Episodio);
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

        public int EditImageInDataBase(HttpPostedFileBase file, Episodio episodio)
        {
            episodio.Arte = ConvertToBytes(file);
            var Episodio = new Episodio
            {
                id_episodio = episodio.id_episodio,
                Titulo = episodio.Titulo,
                Descriçao = episodio.Descriçao,
                Duraçao = episodio.Duraçao,
                Link = episodio.Link,
                Arte = episodio.Arte,
                Data_Public = episodio.Data_Public,
                Participantes = episodio.Participantes
            };
            db.Entry(episodio).State = EntityState.Modified;
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