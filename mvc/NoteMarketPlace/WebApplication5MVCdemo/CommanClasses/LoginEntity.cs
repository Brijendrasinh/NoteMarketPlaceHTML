using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5MVCdemo.Models;

namespace NoteMarketPlace.CommanClasses
{
    public class LoginEntity
    {
        NoteMarketPlaceEntities db = new NoteMarketPlaceEntities();
        public bool UpdateVerifyEmail(string Email)
        {
            bool Result = false;
            
            try
            {
                var count = db.Users.Where(x => x.EmailID.Equals(Email) && x.IsEmailVerified == true).Count();
                if (count == 1)
                {
                    Result = true;
                }
                else if(count == 0) {
                    Result = false;
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
            }
            return Result;
        }
        public bool CheckIfUserExistOrNot(string Email)
        {
            bool Result = false;

            try
            {
                //String email = String.Empty;
                var email =db.Users.Where(x => x.EmailID == Email && x.IsEmailVerified == true).Count();

                if( email == 1 )
                {
                    Result = true;
                }
                else if (email == 0)
                {
                    Result = false;
                }
        
            }
             
            catch (Exception ex)
            {
                string message = ex.Message;
            }

            return Result;
        }

        public bool EmailExistOrNot(string Email)
        {
            bool Result = false;
            try
            {
                var emailCount = db.Users.Where(x => x.EmailID == Email).Count();
                if(emailCount == 1)
                {
                    Result = true;
                }
                else if (emailCount == 0)
                {
                    Result = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Result;
        }

    }
}