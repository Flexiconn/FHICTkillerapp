using System;
using Common;

namespace Logic
{
    public class Test
    {
        Querries Querries = new Querries();

        public void newPost(Posts posts) 
        {
            Querries.AddPost(posts);

        }
       
        
    }
}
