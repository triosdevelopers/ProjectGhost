using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGhost.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CameraSchedule",
                columns: table => new
                {
                    CameraScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OnTime = table.Column<int>(nullable: false),
                    OffTime = table.Column<int>(nullable: false),
                    CameraState = table.Column<bool>(nullable: false, defaultValue: false),
                    CaptureType = table.Column<int>(nullable: false, defaultValue: 0),
                    RecordingDuration = table.Column<int>(nullable: false, defaultValue: 0),
                    RecordingDelay = table.Column<int>(nullable: false, defaultValue: 0),
                    SnapshotCount = table.Column<int>(nullable: false, defaultValue: 0),
                    SnapshotDelay = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraSchedule", x => x.CameraScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "GhostType",
                columns: table => new
                {
                    GhostTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Features = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GhostType", x => x.GhostTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Ghost",
                columns: table => new
                {
                    GhostID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<int>(nullable: false),
                    GhostTypeID = table.Column<int>(nullable: false),
                    CameraScheduleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghost", x => x.GhostID);
                    table.ForeignKey(
                        name: "FK_Ghost_CameraSchedule_CameraScheduleID",
                        column: x => x.CameraScheduleID,
                        principalTable: "CameraSchedule",
                        principalColumn: "CameraScheduleID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Ghost_GhostType_GhostTypeID",
                        column: x => x.GhostTypeID,
                        principalTable: "GhostType",
                        principalColumn: "GhostTypeID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Capture",
                columns: table => new
                {
                    CaptureID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<bool>(nullable: false),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capture", x => x.CaptureID);
                    table.ForeignKey(
                        name: "FK_Capture_Ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "Ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GhostProtocol",
                columns: table => new
                {
                    GhostProtocolsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CameraBrightness = table.Column<int>(nullable: false, defaultValue: 20),
                    CameraContrast = table.Column<int>(nullable: false, defaultValue: 50),
                    CameraState = table.Column<bool>(nullable: false, defaultValue: false),
                    LedBrightness = table.Column<int>(nullable: false, defaultValue: 20),
                    LedState = table.Column<bool>(nullable: false, defaultValue: false),
                    Volume = table.Column<int>(nullable: false, defaultValue: 20),
                    SpeakerState = table.Column<bool>(nullable: false, defaultValue: false),
                    MotionSensorState = table.Column<bool>(nullable: false, defaultValue: false),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GhostProtocol", x => x.GhostProtocolsID);
                    table.ForeignKey(
                        name: "FK_GhostProtocol_Ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "Ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 2048, nullable: false),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "Ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Capture_GhostID",
                table: "Capture",
                column: "GhostID");

            migrationBuilder.CreateIndex(
                name: "IX_Ghost_CameraScheduleID",
                table: "Ghost",
                column: "CameraScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Ghost_GhostTypeID",
                table: "Ghost",
                column: "GhostTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_GhostProtocol_GhostID",
                table: "GhostProtocol",
                column: "GhostID");

            migrationBuilder.CreateIndex(
                name: "IX_User_GhostID",
                table: "User",
                column: "GhostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Capture");

            migrationBuilder.DropTable(
                name: "GhostProtocol");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Ghost");

            migrationBuilder.DropTable(
                name: "CameraSchedule");

            migrationBuilder.DropTable(
                name: "GhostType");
        }
    }
}
