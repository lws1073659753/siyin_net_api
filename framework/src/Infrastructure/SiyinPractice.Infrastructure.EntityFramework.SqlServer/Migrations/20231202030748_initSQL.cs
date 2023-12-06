using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiyinPractice.Infrastructure.EntityFramework.SqlServer.Migrations
{
    public partial class initSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "base_area",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "区域代码"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_area", x => x.Id);
                },
                comment: "区域");

            migrationBuilder.CreateTable(
                name: "base_project",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoxPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "box前缀"),
                    TAG_NOPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "TAG_NO前缀"),
                    SnReplace = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "条码替换"),
                    EndShield = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "末尾屏蔽"),
                    IsPlantSn = table.Column<bool>(type: "bit", nullable: true, comment: "是否厂内sn"),
                    CreateDept = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "导入部门"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_project", x => x.Id);
                },
                comment: "项目");

            migrationBuilder.CreateTable(
                name: "base_warehouse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true, comment: "客户代码"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_warehouse", x => x.Id);
                },
                comment: "库位");

            migrationBuilder.CreateTable(
                name: "LoginLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Device = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Succeed = table.Column<bool>(type: "bit", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LogName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Succeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysCfg",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysCfg", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysDept",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Pids = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SimpleName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Tips = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDept", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysDict",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysDict", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Component = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Hidden = table.Column<bool>(type: "bit", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    IsMenu = table.Column<bool>(type: "bit", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: true),
                    Levels = table.Column<int>(type: "int", nullable: false),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    PCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    PCodes = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Tips = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysNloglogProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TraceIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseDir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QueryContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysNloglogProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysNotice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysNotice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ordinal = table.Column<int>(type: "int", nullable: false),
                    Pid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Tips = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeptId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    RoleIds = table.Column<string>(type: "nvarchar(72)", maxLength: 72, nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WeChat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DingDing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Editor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysUser_SysDept_DeptId",
                        column: x => x.DeptId,
                        principalTable: "SysDept",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LoggerLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logger = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThreadID = table.Column<int>(type: "int", nullable: false),
                    ThreadName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcessID = table.Column<int>(type: "int", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoggerLog_SysNloglogProperty_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "SysNloglogProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysRelation_SysMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysRelation_SysRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoggerLog_PropertiesId",
                table: "LoggerLog",
                column: "PropertiesId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRelation_MenuId",
                table: "SysRelation",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysRelation_RoleId",
                table: "SysRelation",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysUser_DeptId",
                table: "SysUser",
                column: "DeptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "base_area");

            migrationBuilder.DropTable(
                name: "base_project");

            migrationBuilder.DropTable(
                name: "base_warehouse");

            migrationBuilder.DropTable(
                name: "LoggerLog");

            migrationBuilder.DropTable(
                name: "LoginLog");

            migrationBuilder.DropTable(
                name: "OperationLog");

            migrationBuilder.DropTable(
                name: "SysCfg");

            migrationBuilder.DropTable(
                name: "SysDict");

            migrationBuilder.DropTable(
                name: "SysNotice");

            migrationBuilder.DropTable(
                name: "SysRelation");

            migrationBuilder.DropTable(
                name: "SysUser");

            migrationBuilder.DropTable(
                name: "SysNloglogProperty");

            migrationBuilder.DropTable(
                name: "SysMenu");

            migrationBuilder.DropTable(
                name: "SysRole");

            migrationBuilder.DropTable(
                name: "SysDept");
        }
    }
}
