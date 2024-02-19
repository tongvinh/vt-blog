﻿using VTBlog.Core.Models.Content;
using VTBlog.Core.Models;

namespace VTBlog.WebApp.Models
{
    public class ListPostByUserViewModel
    {
        public string Keyword { get; set; }
        public int TotalPosts { get; set; }
        public int TotalDraftPosts { get; set; }
        public int TotalWaitingApprovalPosts { get; set; }
        public int TotalPublishedPosts { get; set; }
        public int TotalUnpaidPosts { get; set; }
        public double TotalUnpaidAmount { get; set; }
        public double TotalPaidAmount { get; set; }
        public PagedResult<PostInListDto>  Posts { get; set; }
    }
}
