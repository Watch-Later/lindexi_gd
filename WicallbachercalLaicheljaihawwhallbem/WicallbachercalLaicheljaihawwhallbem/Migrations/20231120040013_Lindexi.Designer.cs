﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WicallbachercalLaicheljaihawwhallbem;

#nullable disable

namespace WicallbachercalLaicheljaihawwhallbem.Migrations
{
    [DbContext(typeof(FileStorageContext))]
    [Migration("20231120040013_Lindexi")]
    partial class Lindexi
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("WicallbachercalLaicheljaihawwhallbem.FileStorageModel", b =>
                {
                    b.Property<string>("FileSha1Hash")
                        .HasColumnType("TEXT");

                    b.Property<long>("FileLength")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OriginFilePath")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("ReferenceCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("FileSha1Hash");

                    b.ToTable("FileStorageModel");
                });
#pragma warning restore 612, 618
        }
    }
}
