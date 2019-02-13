using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectGhost.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cameraSchedule",
                columns: table => new
                {
                    CameraScheduleID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OnTime = table.Column<int>(nullable: false),
                    OffTime = table.Column<int>(nullable: false),
                    CameraState = table.Column<int>(nullable: false, defaultValue: 0),
                    CaptureType = table.Column<int>(nullable: false, defaultValue: 0),
                    RecordingDuration = table.Column<int>(nullable: false, defaultValue: 0),
                    RecordingDelay = table.Column<int>(nullable: false, defaultValue: 0),
                    SnapshotCount = table.Column<int>(nullable: false, defaultValue: 0),
                    SnapshotDelay = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cameraSchedule", x => x.CameraScheduleID);
                });

            migrationBuilder.CreateTable(
                name: "ghostType",
                columns: table => new
                {
                    GhostTypeID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Features = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ghostType", x => x.GhostTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ghost",
                columns: table => new
                {
                    GhostID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(nullable: false),
                    GhostTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ghost", x => x.GhostID);
                    
                    table.ForeignKey(
                        name: "FK_ghost_ghostType_GhostTypeID",
                        column: x => x.GhostTypeID,
                        principalTable: "ghostType",
                        principalColumn: "GhostTypeID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "capture",
                columns: table => new
                {
                    CaptureID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_capture", x => x.CaptureID);
                    table.ForeignKey(
                        name: "FK_capture_ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ghostProtocol",
                columns: table => new
                {
                    GhostProtocolsID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CameraBrightness = table.Column<int>(nullable: false, defaultValue: 20),
                    CameraContrast = table.Column<int>(nullable: false, defaultValue: 50),
                    CameraState = table.Column<int>(nullable: false, defaultValue: 0),
                    MicState = table.Column<int>(nullable: false, defaultValue: 0),
                    LedState = table.Column<int>(nullable: false, defaultValue: 0),
                    Volume = table.Column<int>(nullable: false, defaultValue: 20),
                    SpeakerState = table.Column<int>(nullable: false, defaultValue: 0),
                    ProximityState = table.Column<int>(nullable: false, defaultValue: 0),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ghostProtocol", x => x.GhostProtocolsID);
                    table.ForeignKey(
                        name: "FK_ghostProtocol_ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 2048, nullable: false),
                    GhostID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_user_ghost_GhostID",
                        column: x => x.GhostID,
                        principalTable: "ghost",
                        principalColumn: "GhostID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_capture_GhostID",
                table: "capture",
                column: "GhostID");

            migrationBuilder.CreateIndex(
                name: "IX_ghost_CameraScheduleID",
                table: "ghost",
                column: "CameraScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_ghost_GhostTypeID",
                table: "ghost",
                column: "GhostTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ghostProtocol_GhostID",
                table: "ghostProtocol",
                column: "GhostID");

            migrationBuilder.CreateIndex(
                name: "IX_user_GhostID",
                table: "user",
                column: "GhostID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "capture");

            migrationBuilder.DropTable(
                name: "ghostProtocol");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "ghost");

            migrationBuilder.DropTable(
                name: "cameraSchedule");

            migrationBuilder.DropTable(
                name: "ghostType");
        }
    }
}
