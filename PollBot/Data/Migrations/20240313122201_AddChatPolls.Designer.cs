﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PollBot.Data;

#nullable disable

namespace PollBot.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240313122201_AddChatPolls")]
    partial class AddChatPolls
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("PollBot.Data.Models.ChatPoll", b =>
                {
                    b.Property<long>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long?>("LastPollId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ChatId");

                    b.ToTable("ChatPolls");
                });
#pragma warning restore 612, 618
        }
    }
}
