using Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static Factory.Factory;

namespace Logic
{
    public class PostContainer
    {
        readonly Contract.IPost IPost;
        readonly Contract.IAccount IAccount;

        public bool AddPost(IFormFile myImage, string postName, string postDescription, string SessionId)
        {
            if (CheckIfSignedIn(IAccount.GetAccountId(SessionId)))
            {
                if (IPost.PostAmount(SessionId) < 3)
                {
                    string postId = Guid.NewGuid().ToString();
                    IPost.AddPost(postId, postName, postDescription, IAccount.GetAccountId(SessionId));
                    if (myImage != null)
                    {
                        IPost.AddImageToDB(postId, FileControl.AddFileToSystem(myImage, $"post/{postId}"), IAccount.GetAccountId(SessionId));
                    }
                    return true;
                }
                return false;
            }
            else {
                return false;
            }

        }



        public List<Logic.Models.LogicPosts> GetPostsList()
        {
            return LogicListDto.Posts(IPost.GetPosts());
        }


        public Logic.Models.LogicPosts ViewPost(string Id)
        {
            Logic.Models.LogicPosts post = new Logic.Models.LogicPosts(IPost.GetPost(Id));
            post.reviews = LogicListDto.Reviews(IPost.GetReview(Id));
            return post;
        }

        public bool CheckIfSignedIn(string SessionId)
        {
            if (IAccount.CheckIfSignedIn(SessionId))
            {
                return true;
            }

            return false;

        }




        public bool createReview(string text, int score, string postId, string SessionId)
        {
            if (CheckIfSignedIn(SessionId))
            {
                IPost.createReview(IAccount.GetAccountId(SessionId), text,  score,  postId);
                return true;
            }
            return false;
        }


        

        public bool FavouriteToggle(string PostId, string AccountId) {
            if (CheckIfSignedIn(AccountId))
            {
                if (IPost.GetFavourites(IAccount.GetAccountId(AccountId)).Find(x => x.Post.PostId == PostId) == null)
                {
                    IPost.AddFavourite(Guid.NewGuid().ToString(), IAccount.GetAccountId(AccountId), PostId);
                    return true;
                }
                else
                {
                    IPost.RemoveFavourite(IPost.GetFavourites(IAccount.GetAccountId(AccountId)).Find(x => x.Post.PostId == PostId).Id);
                    return true;
                }
                
            }
            return false;
        }


        public List<Logic.Models.LogicFavourite> GetFavourites(string AccountId)
        {

            return LogicListDto.Favourites(IPost.GetFavourites(IAccount.GetAccountId(AccountId)));
  
        }
        public PostContainer() {
            IPost = Factory.Factory.GetPostDAL();
            IAccount = Factory.Factory.GetAccountDAL();
        }
        public PostContainer(string mode) {
            if (mode == "Mock") {
                IPost = Factory.MockFactory.GetPostDAL();
                IAccount = Factory.MockFactory.GetAccountDAL();
            }
        }

    }
}
