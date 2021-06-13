using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class FileControl
    {
        public void AddFileToSystem(IFormFile file, string path) {
            
            if (file != null)
            {
                string pathString = System.IO.Path.Combine("wwwroot/data/IMG/post/", path);
                System.IO.Directory.CreateDirectory(pathString);
                myImage.CopyTo(new FileStream(System.IO.Path.Combine(pathString, myImage.FileName.ToString()), FileMode.Create));
            }




            string query = $"INSERT INTO post (PostId, PostName, PostDescription, PostAuthor) VALUES(@PostId, @PostName, @PostDescription, @PostAuthor); ";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@PostId", postId);
            cmd.Parameters.AddWithValue("@PostName", postName);
            cmd.Parameters.AddWithValue("@PostDescription", postDescription);
            cmd.Parameters.AddWithValue("@PostAuthor", Id);

            //Create a data reader and Execute the command
            cmd.ExecuteNonQuery();

            if (myImage != null)
            {
                //posts.PostFileName = System.IO.Path.Combine(pathString, insertPost.MyImage.FileName.ToString());
                query = $"INSERT INTO images (Id, Path, Parent) VALUES('{Guid.NewGuid().ToString()}', @Path, '{postId}'); ";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Path", System.IO.Path.Combine(System.IO.Path.Combine("/data/IMG/post/", postId + "/"), myImage.FileName.ToString()));

                //Create a data reader and Execute the command
                cmd.ExecuteNonQuery();
            }
            close();
        
        }
    }
}
