using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess
{
    public class TwitterAppDbContext:DbContext
    {

        public TwitterAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<Image> Images { get; set; }
        public DbSet<PostDetails> PostDetails { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserLike> UserLikes { get; set; }
        public DbSet<UserShare> UserShares { get; set; }
        public DbSet<UserRetweet> UserRetweets { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostShare> PostShares { get; set; }
        public DbSet<PostRetweet> PostRetweets { get; set; }
        public DbSet<PostComment> PostComments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserComment>()
               .HasOne(x => x.User)
               .WithMany(x => x.UserComments)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserLike>()
              .HasOne(x => x.User)
              .WithMany(x => x.UserLikes)
              .HasForeignKey(x=>x.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        FirstName ="Jane",
                        LastName = "Doe",
                        Username = "jane_doe",
                        Email = "jane.doe@gmail.com",
                        Password = "?B??:q?m?0???eV" ,
                        PhotoUrl = "https://i.pinimg.com/originals/7b/59/6c/7b596c482516d29832b49a76f5632882.jpg"
                    },
                    new User()
                    {
                        Id = 2,
                        FirstName = "John",
                        LastName = "Doe",
                        Username = "john_doe",
                        Email = "john.doe@gmail.com",
                        Password = "?B??:q?m?0???eV",
                        PhotoUrl = "https://i.pinimg.com/236x/1e/bb/b5/1ebbb5240b0d5297fff25c9dc6606ee2--college-hairstyles-hairstyles-for-guys.jpg"
                    });

            //modelBuilder.Entity<Post>()
            //    .HasData(
            //        new Post()
            //        {
            //            Id = 1,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 1,
            //                    Text = "The road to success is by serving the masses",
                                
            //                }
            //            }
            //        },
            //        new Post()
            //        {
            //            Id = 2,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 2,
            //                    Text = "The only constant thing in this world is change",
                                
            //                }
            //            }
            //        },
            //        new Post()
            //        {
            //            Id = 3,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 3,
            //                    Text = "Don't give up on the person you are becoming",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 4,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 4,
            //                    Text = "I feel like making dreams come true",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 5,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 5,
            //                    Text = "was born to be sweet",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 6,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 6,
            //                    Text = "Countries to visit this year: Germany, Austria, Italy",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 7,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 7,
            //                    Text = "Cities to visit this year: Amsterdam, Seychelles, Rome",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 8,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 8,
            //                    Text = "It's a beautiful world",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 9,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 9,
            //                    Text = "I am free",
                                
            //                }
            //            }
            //        }, new Post()
            //        {
            //            Id = 10,
            //            UserId = 1,
            //            PostDetails = new List<PostDetails>()
            //            {
            //                new PostDetails()
            //                {
            //                    Id = 10,
            //                    Text = "Hey you :)",
                                
            //                }
            //            }
            //        }

            //        );

        }
        }
    }
