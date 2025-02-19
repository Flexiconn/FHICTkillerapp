﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    static public class LogicListDto
    {
        static public List<Logic.Models.LogicReview> Reviews(List<Contract.Models.ContractReview> review) {
            List<Logic.Models.LogicReview> reviews = new List<Logic.Models.LogicReview>();
            foreach (var t in review) {
                reviews.Add(new Logic.Models.LogicReview(t));
            }
            return reviews;
        }

        static public List<Logic.Models.LogicOrder> Orders(List<Contract.Models.Contractorder> order)
        {
            List<Logic.Models.LogicOrder> orders = new List<Logic.Models.LogicOrder>();
            foreach (var t in order)
            {
                orders.Add(new Logic.Models.LogicOrder(t));
            }
            return orders;
        }

        static public List<Logic.Models.LogicFavourite> Favourites(List<Contract.Models.ContractFavourite> order)
        {
            List<Logic.Models.LogicFavourite> orders = new List<Logic.Models.LogicFavourite>();
            foreach (var t in order)
            {
                orders.Add(new Logic.Models.LogicFavourite(t));
            }
            return orders;
        }

        static public List<Logic.Models.LogicReport> Reports(List<Contract.Models.ContractReport> report)
        {
            List<Logic.Models.LogicReport> reports = new List<Logic.Models.LogicReport>();
            foreach (var t in report)
            {
                reports.Add(new Logic.Models.LogicReport(t));
            }
            return reports;
        }

        static public List<Logic.Models.LogicClientChat> Messages(List<Contract.Models.ContractClientChat> message)
        {
            List<Logic.Models.LogicClientChat> messages = new List<Logic.Models.LogicClientChat>();
            foreach (var t in message)
            {
                messages.Add(new Logic.Models.LogicClientChat(t));
            }
            return messages;
        }

        static public List<Logic.Models.LogicPosts> Posts(List<Contract.Models.ContractPosts> message)
        {
            List<Logic.Models.LogicPosts> messages = new List<Logic.Models.LogicPosts>();
            foreach (var t in message)
            {
                messages.Add(new Logic.Models.LogicPosts(t));
            }
            return messages;
        }
    }
}
