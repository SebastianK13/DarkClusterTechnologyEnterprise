using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkClusterTechnologyEnterprise.Migrations.AppServiceDeskDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountForms",
                columns: table => new
                {
                    AccountRequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    SuperiorId = table.Column<int>(nullable: false),
                    TimeZoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountForms", x => x.AccountRequestId);
                });

            migrationBuilder.CreateTable(
                name: "AssigmentGroup",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssigmentGroup", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CloserDues",
                columns: table => new
                {
                    CloserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Due = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloserDues", x => x.CloserId);
                });

            migrationBuilder.CreateTable(
                name: "Impacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Urgencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urgencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    ServiceWorkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ResponsibleEmployee = table.Column<int>(nullable: false),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.ServiceWorkId);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                    table.ForeignKey(
                        name: "FK_Members_AssigmentGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AssigmentGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categorizations",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorizations", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Categorizations_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RequestedPerson = table.Column<int>(nullable: false),
                    ContactPerson = table.Column<int>(nullable: false),
                    ImpactId = table.Column<int>(nullable: false),
                    UrgencyId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Assignee = table.Column<string>(nullable: true),
                    AccountFormId = table.Column<int>(nullable: true),
                    HistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_AccountForms_AccountFormId",
                        column: x => x.AccountFormId,
                        principalTable: "AccountForms",
                        principalColumn: "AccountRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Categorizations_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorizations",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_AssigmentGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AssigmentGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Impacts_ImpactId",
                        column: x => x.ImpactId,
                        principalTable: "Impacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Urgencies_UrgencyId",
                        column: x => x.UrgencyId,
                        principalTable: "Urgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    User = table.Column<string>(nullable: false),
                    Administrator = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    MessageDate = table.Column<DateTime>(nullable: false),
                    RequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.User);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    RequestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RequestedPerson = table.Column<int>(nullable: false),
                    ContactPerson = table.Column<int>(nullable: false),
                    ImpactId = table.Column<int>(nullable: false),
                    UrgencyId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    Assignee = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    HistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Applications_Categorizations_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorizations",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_AssigmentGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AssigmentGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Impacts_ImpactId",
                        column: x => x.ImpactId,
                        principalTable: "Impacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Urgencies_UrgencyId",
                        column: x => x.UrgencyId,
                        principalTable: "Urgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RequestedPerson = table.Column<int>(nullable: false),
                    ContactPerson = table.Column<int>(nullable: false),
                    ImpactId = table.Column<int>(nullable: false),
                    UrgencyId = table.Column<int>(nullable: false),
                    PriorityId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true),
                    Assignee = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    HistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentId);
                    table.ForeignKey(
                        name: "FK_Incidents_Categorizations_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorizations",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_AssigmentGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AssigmentGroup",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_Impacts_ImpactId",
                        column: x => x.ImpactId,
                        principalTable: "Impacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Urgencies_UrgencyId",
                        column: x => x.UrgencyId,
                        principalTable: "Urgencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatusHistory",
                columns: table => new
                {
                    ChangeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Solution = table.Column<string>(nullable: true),
                    CloserId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusHistory", x => x.ChangeId);
                    table.ForeignKey(
                        name: "FK_StatusHistory_CloserDues_CloserId",
                        column: x => x.CloserId,
                        principalTable: "CloserDues",
                        principalColumn: "CloserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DueTime = table.Column<DateTime>(nullable: false),
                    Expired = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Statuses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Statuses_StatusHistory_Status",
                        column: x => x.Status,
                        principalTable: "StatusHistory",
                        principalColumn: "ChangeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupId",
                table: "Applications",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_HistoryId",
                table: "Applications",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ImpactId",
                table: "Applications",
                column: "ImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PriorityId",
                table: "Applications",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UrgencyId",
                table: "Applications",
                column: "UrgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorizations_CategoryId",
                table: "Categorizations",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RequestId",
                table: "Conversations",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CategoryId",
                table: "Incidents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_GroupId",
                table: "Incidents",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_HistoryId",
                table: "Incidents",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_ImpactId",
                table: "Incidents",
                column: "ImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_PriorityId",
                table: "Incidents",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_UrgencyId",
                table: "Incidents",
                column: "UrgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StateId",
                table: "Statuses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Status",
                table: "Statuses",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistory_CloserId",
                table: "StatusHistory",
                column: "CloserId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusHistory_StatusId",
                table: "StatusHistory",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AccountFormId",
                table: "Tasks",
                column: "AccountFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_GroupId",
                table: "Tasks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_HistoryId",
                table: "Tasks",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ImpactId",
                table: "Tasks",
                column: "ImpactId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PriorityId",
                table: "Tasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UrgencyId",
                table: "Tasks",
                column: "UrgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_StatusHistory_HistoryId",
                table: "Tasks",
                column: "HistoryId",
                principalTable: "StatusHistory",
                principalColumn: "ChangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Applications_RequestId",
                table: "Conversations",
                column: "RequestId",
                principalTable: "Applications",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_StatusHistory_HistoryId",
                table: "Applications",
                column: "HistoryId",
                principalTable: "StatusHistory",
                principalColumn: "ChangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Incidents_StatusHistory_HistoryId",
                table: "Incidents",
                column: "HistoryId",
                principalTable: "StatusHistory",
                principalColumn: "ChangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusHistory_Statuses_StatusId",
                table: "StatusHistory",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_StatusHistory_Status",
                table: "Statuses");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "AccountForms");

            migrationBuilder.DropTable(
                name: "Categorizations");

            migrationBuilder.DropTable(
                name: "AssigmentGroup");

            migrationBuilder.DropTable(
                name: "Impacts");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Urgencies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StatusHistory");

            migrationBuilder.DropTable(
                name: "CloserDues");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
