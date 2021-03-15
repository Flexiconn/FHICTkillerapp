using System;
using Common.Models;

namespace Logic
{
    public class Test
    {
        Querries Querries = new Querries();

        public void newPost(PostUpload posts) 
        {
            Querries.AddPost(posts);

        }
       
        
    }
}
