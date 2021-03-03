using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Logic
{
    class Querries
    {
        CliverrContext db = new CliverrContext();
        public void AddPost(Posts posts) 
        {
            db.Add(posts);
        }
    }
}
