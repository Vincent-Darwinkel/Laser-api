﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace LaserAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animation",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animation", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Lasershow",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lasershow", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Pattern",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Scale = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pattern", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    MaxLaserPowerInZonePwm = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "PatternAnimation",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnimationUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTimeOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternAnimation", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_PatternAnimation_Animation_AnimationUuid",
                        column: x => x.AnimationUuid,
                        principalTable: "Animation",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LasershowAnimation",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    LasershowUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    StartTimeOffset = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnimationUuid = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LasershowAnimation", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_LasershowAnimation_Animation_AnimationUuid",
                        column: x => x.AnimationUuid,
                        principalTable: "Animation",
                        principalColumn: "Uuid");
                    table.ForeignKey(
                        name: "FK_LasershowAnimation_Lasershow_LasershowUuid",
                        column: x => x.LasershowUuid,
                        principalTable: "Lasershow",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PointDto",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatternUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    RedLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    GreenLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    BlueLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    PatternDtoUuid = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointDto", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_PointDto_Pattern_PatternDtoUuid",
                        column: x => x.PatternDtoUuid,
                        principalTable: "Pattern",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateTable(
                name: "ZonePosition",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    ZoneUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonePosition", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_ZonePosition_Zone_ZoneUuid",
                        column: x => x.ZoneUuid,
                        principalTable: "Zone",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatternAnimationSetting",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatternAnimationUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Scale = table.Column<double>(type: "REAL", nullable: false),
                    Rotation = table.Column<int>(type: "INTEGER", nullable: false),
                    CenterX = table.Column<int>(type: "INTEGER", nullable: false),
                    CenterY = table.Column<int>(type: "INTEGER", nullable: false),
                    StartTime = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatternAnimationSetting", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_PatternAnimationSetting_PatternAnimation_PatternAnimationUuid",
                        column: x => x.PatternAnimationUuid,
                        principalTable: "PatternAnimation",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimationPoint",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatternAnimationSettingsUuid = table.Column<Guid>(type: "TEXT", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    RedLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    GreenLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    BlueLaserPowerPwm = table.Column<int>(type: "INTEGER", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimationPoint", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_AnimationPoint_PatternAnimationSetting_PatternAnimationSettingsUuid",
                        column: x => x.PatternAnimationSettingsUuid,
                        principalTable: "PatternAnimationSetting",
                        principalColumn: "Uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimationPoint_PatternAnimationSettingsUuid",
                table: "AnimationPoint",
                column: "PatternAnimationSettingsUuid");

            migrationBuilder.CreateIndex(
                name: "IX_LasershowAnimation_AnimationUuid",
                table: "LasershowAnimation",
                column: "AnimationUuid");

            migrationBuilder.CreateIndex(
                name: "IX_LasershowAnimation_LasershowUuid",
                table: "LasershowAnimation",
                column: "LasershowUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PatternAnimation_AnimationUuid",
                table: "PatternAnimation",
                column: "AnimationUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PatternAnimationSetting_PatternAnimationUuid",
                table: "PatternAnimationSetting",
                column: "PatternAnimationUuid");

            migrationBuilder.CreateIndex(
                name: "IX_PointDto_PatternDtoUuid",
                table: "PointDto",
                column: "PatternDtoUuid");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePosition_ZoneUuid",
                table: "ZonePosition",
                column: "ZoneUuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimationPoint");

            migrationBuilder.DropTable(
                name: "LasershowAnimation");

            migrationBuilder.DropTable(
                name: "PointDto");

            migrationBuilder.DropTable(
                name: "ZonePosition");

            migrationBuilder.DropTable(
                name: "PatternAnimationSetting");

            migrationBuilder.DropTable(
                name: "Lasershow");

            migrationBuilder.DropTable(
                name: "Pattern");

            migrationBuilder.DropTable(
                name: "Zone");

            migrationBuilder.DropTable(
                name: "PatternAnimation");

            migrationBuilder.DropTable(
                name: "Animation");
        }
    }
}
