using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WOMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedSingularTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_AspNetUsers_UserId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_Assets_AssetId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_Locations_FromLocationId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_Locations_ToLocationId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistories_WorkOrders_WorkOrderId",
                table: "AssetHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Locations_CurrentLocationId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_WorkOrders_WorkOrderId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateSkills_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateSkills_Skills_SkillId",
                table: "AssignmentTemplateSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateTechnicians_AspNetUsers_TechnicianId",
                table: "AssignmentTemplateTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateTechnicians_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateTechnicians");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateWorkTypes_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateWorkTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateWorkTypes_WorkOrderTypes_WorkOrderTypeId",
                table: "AssignmentTemplateWorkTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateZones_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateZones");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateZones_Zones_ZoneId",
                table: "AssignmentTemplateZones");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCountItems_CycleCounts_CycleCountId",
                table: "CycleCountItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCountItems_Inventory_InventoryId",
                table: "CycleCountItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCounts_AspNetUsers_CountedByUserId",
                table: "CycleCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCounts_Locations_LocationId",
                table: "CycleCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_AspNetUsers_UserId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispositions_AspNetUsers_DisposedByUserId",
                table: "InventoryDispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispositions_Inventory_InventoryId",
                table: "InventoryDispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDispositions_Vendors_VendorId",
                table: "InventoryDispositions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLocations_Inventory_InventoryId",
                table: "InventoryLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLocations_Locations_LocationId",
                table: "InventoryLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_AspNetUsers_UserId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Inventory_InventoryId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Locations_FromLocationId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransactions_Locations_ToLocationId",
                table: "InventoryTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKitItems_Inventory_InventoryId",
                table: "JobKitItems");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKitItems_JobKits_JobKitId",
                table: "JobKitItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_ManagerId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Locations_ParentLocationId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplates_AspNetUsers_UserId",
                table: "NotificationTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Routes_AspNetUsers_DriverId",
                table: "Routes");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_Routes_RouteId",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStops_WorkOrders_WorkOrderId",
                table: "RouteStops");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLogs_AspNetUsers_ScannedByUserId",
                table: "ScanLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLogs_Assets_AssetId",
                table: "ScanLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLogs_Inventory_InventoryId",
                table: "ScanLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_SentNotifications_AspNetUsers_UserId",
                table: "SentNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SentNotifications_NotificationTemplates_TemplateId",
                table: "SentNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianEquipments_AspNetUsers_TechnicianId",
                table: "TechnicianEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianEquipments_Equipment_EquipmentId",
                table: "TechnicianEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianSkills_AspNetUsers_TechnicianId",
                table: "TechnicianSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianSkills_Skills_SkillId",
                table: "TechnicianSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianZones_AspNetUsers_TechnicianId",
                table: "TechnicianZones");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianZones_Zones_ZoneId",
                table: "TechnicianZones");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationIssues_AspNetUsers_ResolvedByUserId",
                table: "ValidationIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationIssues_Assets_AssetId",
                table: "ValidationIssues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowActions_Workflows_WorkflowId",
                table: "WorkflowActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignments_AspNetUsers_AssigneeId",
                table: "WorkflowAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignments_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignments_Workflows_WorkflowId",
                table: "WorkflowAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdges_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowEdges");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdges_WorkflowNodes_WorkflowNodeId1",
                table: "WorkflowEdges");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdges_Workflows_WorkflowId",
                table: "WorkflowEdges");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowForms_Forms_FormId",
                table: "WorkflowForms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowForms_Workflows_WorkflowId",
                table: "WorkflowForms");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstances_AspNetUsers_CreatedBy",
                table: "WorkflowInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstances_Workflows_WorkflowId",
                table: "WorkflowInstances");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstanceSteps_WorkflowInstances_InstanceId",
                table: "WorkflowInstanceSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstanceSteps_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowInstanceSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowNodes_Workflows_WorkflowId",
                table: "WorkflowNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowProgresses_WorkflowStatuses_CurrentStatusId",
                table: "WorkflowProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowProgresses_Workflows_WorkflowId",
                table: "WorkflowProgresses");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRuleActions_WorkflowRules_RuleId",
                table: "WorkflowRuleActions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRuleConditions_WorkflowRules_RuleId",
                table: "WorkflowRuleConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRules_Workflows_WorkflowId",
                table: "WorkflowRules");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_AspNetUsers_CreatedBy",
                table: "Workflows");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStatuses_Workflows_WorkflowId",
                table: "WorkflowStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransitions_WorkflowStatuses_FromStatusId",
                table: "WorkflowTransitions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransitions_WorkflowStatuses_ToStatusId",
                table: "WorkflowTransitions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransitions_Workflows_WorkflowId",
                table: "WorkflowTransitions");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAssignments_WorkOrders_WorkOrderId",
                table: "WorkOrderAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAttachments_AspNetUsers_UploadedBy",
                table: "WorkOrderAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAttachments_WorkOrders_WorkOrderId",
                table: "WorkOrderAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEquipmentRequirements_Equipment_EquipmentId",
                table: "WorkOrderEquipmentRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEquipmentRequirements_WorkOrders_WorkOrderId",
                table: "WorkOrderEquipmentRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AspNetUsers_AssignedTechnicianId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AspNetUsers_CreatedBy",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_WorkflowStatuses_WorkflowStatusId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Workflows_WorkflowId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderSkillRequirements_Skills_SkillId",
                table: "WorkOrderSkillRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderSkillRequirements_WorkOrders_WorkOrderId",
                table: "WorkOrderSkillRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirements_Equipment_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirements_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeSkillRequirements_Skills_SkillId",
                table: "WorkOrderTypeSkillRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeSkillRequirements_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderZones_WorkOrders_WorkOrderId",
                table: "WorkOrderZones");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderZones_Zones_ZoneId",
                table: "WorkOrderZones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zones",
                table: "Zones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderZones",
                table: "WorkOrderZones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderTypeSkillRequirements",
                table: "WorkOrderTypeSkillRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderTypes",
                table: "WorkOrderTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderTypeEquipmentRequirements",
                table: "WorkOrderTypeEquipmentRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderSkillRequirements",
                table: "WorkOrderSkillRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrders",
                table: "WorkOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderEquipmentRequirements",
                table: "WorkOrderEquipmentRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderAttachments",
                table: "WorkOrderAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderAssignments",
                table: "WorkOrderAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowTransitions",
                table: "WorkflowTransitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowStatuses",
                table: "WorkflowStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRules",
                table: "WorkflowRules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRuleConditions",
                table: "WorkflowRuleConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRuleActions",
                table: "WorkflowRuleActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowProgresses",
                table: "WorkflowProgresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowNodes",
                table: "WorkflowNodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowInstanceSteps",
                table: "WorkflowInstanceSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowInstances",
                table: "WorkflowInstances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowForms",
                table: "WorkflowForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowEdges",
                table: "WorkflowEdges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowAssignments",
                table: "WorkflowAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowActions",
                table: "WorkflowActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationIssues",
                table: "ValidationIssues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianZones",
                table: "TechnicianZones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianSkills",
                table: "TechnicianSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianEquipments",
                table: "TechnicianEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SentNotifications",
                table: "SentNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanLogs",
                table: "ScanLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteStops",
                table: "RouteStops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplates",
                table: "NotificationTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKits",
                table: "JobKits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKitItems",
                table: "JobKitItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryTransactions",
                table: "InventoryTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryLocations",
                table: "InventoryLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryDispositions",
                table: "InventoryDispositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forms",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CycleCounts",
                table: "CycleCounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CycleCountItems",
                table: "CycleCountItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateZones",
                table: "AssignmentTemplateZones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateWorkTypes",
                table: "AssignmentTemplateWorkTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateTechnicians",
                table: "AssignmentTemplateTechnicians");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateSkills",
                table: "AssignmentTemplateSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplates",
                table: "AssignmentTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetHistories",
                table: "AssetHistories");

            migrationBuilder.RenameTable(
                name: "Zones",
                newName: "Zone");

            migrationBuilder.RenameTable(
                name: "WorkOrderZones",
                newName: "WorkOrderZone");

            migrationBuilder.RenameTable(
                name: "WorkOrderTypeSkillRequirements",
                newName: "WorkOrderTypeSkillRequirement");

            migrationBuilder.RenameTable(
                name: "WorkOrderTypes",
                newName: "WorkOrderType");

            migrationBuilder.RenameTable(
                name: "WorkOrderTypeEquipmentRequirements",
                newName: "WorkOrderTypeEquipmentRequirement");

            migrationBuilder.RenameTable(
                name: "WorkOrderSkillRequirements",
                newName: "WorkOrderSkillRequirement");

            migrationBuilder.RenameTable(
                name: "WorkOrders",
                newName: "WorkOrder");

            migrationBuilder.RenameTable(
                name: "WorkOrderEquipmentRequirements",
                newName: "WorkOrderEquipmentRequirement");

            migrationBuilder.RenameTable(
                name: "WorkOrderAttachments",
                newName: "WorkOrderAttachment");

            migrationBuilder.RenameTable(
                name: "WorkOrderAssignments",
                newName: "WorkOrderAssignment");

            migrationBuilder.RenameTable(
                name: "WorkflowTransitions",
                newName: "WorkflowTransition");

            migrationBuilder.RenameTable(
                name: "WorkflowStatuses",
                newName: "WorkflowStatus");

            migrationBuilder.RenameTable(
                name: "Workflows",
                newName: "Workflow");

            migrationBuilder.RenameTable(
                name: "WorkflowRules",
                newName: "WorkflowRule");

            migrationBuilder.RenameTable(
                name: "WorkflowRuleConditions",
                newName: "WorkflowRuleCondition");

            migrationBuilder.RenameTable(
                name: "WorkflowRuleActions",
                newName: "WorkflowRuleAction");

            migrationBuilder.RenameTable(
                name: "WorkflowProgresses",
                newName: "WorkflowProgress");

            migrationBuilder.RenameTable(
                name: "WorkflowNodes",
                newName: "WorkflowNode");

            migrationBuilder.RenameTable(
                name: "WorkflowInstanceSteps",
                newName: "WorkflowInstanceStep");

            migrationBuilder.RenameTable(
                name: "WorkflowInstances",
                newName: "WorkflowInstance");

            migrationBuilder.RenameTable(
                name: "WorkflowForms",
                newName: "WorkflowForm");

            migrationBuilder.RenameTable(
                name: "WorkflowEdges",
                newName: "WorkflowEdge");

            migrationBuilder.RenameTable(
                name: "WorkflowAssignments",
                newName: "WorkflowAssignment");

            migrationBuilder.RenameTable(
                name: "WorkflowActions",
                newName: "WorkflowAction");

            migrationBuilder.RenameTable(
                name: "Vendors",
                newName: "Vendor");

            migrationBuilder.RenameTable(
                name: "ValidationIssues",
                newName: "ValidationIssue");

            migrationBuilder.RenameTable(
                name: "TechnicianZones",
                newName: "TechnicianZone");

            migrationBuilder.RenameTable(
                name: "TechnicianSkills",
                newName: "TechnicianSkill");

            migrationBuilder.RenameTable(
                name: "TechnicianEquipments",
                newName: "TechnicianEquipment");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "Skill");

            migrationBuilder.RenameTable(
                name: "SentNotifications",
                newName: "SentNotification");

            migrationBuilder.RenameTable(
                name: "ScanLogs",
                newName: "ScanLog");

            migrationBuilder.RenameTable(
                name: "RouteStops",
                newName: "RouteStop");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Route");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "NotificationTemplates",
                newName: "NotificationTemplate");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "JobKits",
                newName: "JobKit");

            migrationBuilder.RenameTable(
                name: "JobKitItems",
                newName: "JobKitItem");

            migrationBuilder.RenameTable(
                name: "InventoryTransactions",
                newName: "InventoryTransaction");

            migrationBuilder.RenameTable(
                name: "InventoryLocations",
                newName: "InventoryLocation");

            migrationBuilder.RenameTable(
                name: "InventoryDispositions",
                newName: "InventoryDisposition");

            migrationBuilder.RenameTable(
                name: "Forms",
                newName: "Form");

            migrationBuilder.RenameTable(
                name: "CycleCounts",
                newName: "CycleCount");

            migrationBuilder.RenameTable(
                name: "CycleCountItems",
                newName: "CycleCountItem");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateZones",
                newName: "AssignmentTemplateZone");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateWorkTypes",
                newName: "AssignmentTemplateWorkType");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateTechnicians",
                newName: "AssignmentTemplateTechnician");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateSkills",
                newName: "AssignmentTemplateSkill");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplates",
                newName: "AssignmentTemplate");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Asset");

            migrationBuilder.RenameTable(
                name: "AssetHistories",
                newName: "AssetHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_Type",
                table: "Zone",
                newName: "IX_Zone_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Zones_Name",
                table: "Zone",
                newName: "IX_Zone_Name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderZones_ZoneId",
                table: "WorkOrderZone",
                newName: "IX_WorkOrderZone_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderZones_WorkOrderId",
                table: "WorkOrderZone",
                newName: "IX_WorkOrderZone_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeSkillRequirements_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirement",
                newName: "IX_WorkOrderTypeSkillRequirement_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeSkillRequirements_SkillId",
                table: "WorkOrderTypeSkillRequirement",
                newName: "IX_WorkOrderTypeSkillRequirement_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypes_Name",
                table: "WorkOrderType",
                newName: "IX_WorkOrderType_Name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypes_Category",
                table: "WorkOrderType",
                newName: "IX_WorkOrderType_Category");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeEquipmentRequirements_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirement",
                newName: "IX_WorkOrderTypeEquipmentRequirement_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeEquipmentRequirements_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirement",
                newName: "IX_WorkOrderTypeEquipmentRequirement_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderSkillRequirements_WorkOrderId",
                table: "WorkOrderSkillRequirement",
                newName: "IX_WorkOrderSkillRequirement_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderSkillRequirements_SkillId",
                table: "WorkOrderSkillRequirement",
                newName: "IX_WorkOrderSkillRequirement_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkOrderTypeId",
                table: "WorkOrder",
                newName: "IX_WorkOrder_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkOrderNumber",
                table: "WorkOrder",
                newName: "IX_WorkOrder_WorkOrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkflowStatusId",
                table: "WorkOrder",
                newName: "IX_WorkOrder_WorkflowStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_WorkflowId",
                table: "WorkOrder",
                newName: "IX_WorkOrder_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_Status",
                table: "WorkOrder",
                newName: "IX_WorkOrder_Status");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_Priority",
                table: "WorkOrder",
                newName: "IX_WorkOrder_Priority");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_CreatedBy",
                table: "WorkOrder",
                newName: "IX_WorkOrder_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_AssignedTechnicianId",
                table: "WorkOrder",
                newName: "IX_WorkOrder_AssignedTechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderEquipmentRequirements_WorkOrderId",
                table: "WorkOrderEquipmentRequirement",
                newName: "IX_WorkOrderEquipmentRequirement_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderEquipmentRequirements_EquipmentId",
                table: "WorkOrderEquipmentRequirement",
                newName: "IX_WorkOrderEquipmentRequirement_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachments_WorkOrderId",
                table: "WorkOrderAttachment",
                newName: "IX_WorkOrderAttachment_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachments_UploadedBy",
                table: "WorkOrderAttachment",
                newName: "IX_WorkOrderAttachment_UploadedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachments_FileName",
                table: "WorkOrderAttachment",
                newName: "IX_WorkOrderAttachment_FileName");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignments_WorkOrderId",
                table: "WorkOrderAssignment",
                newName: "IX_WorkOrderAssignment_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignments_Identifier",
                table: "WorkOrderAssignment",
                newName: "IX_WorkOrderAssignment_Identifier");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignments_AccountNumber",
                table: "WorkOrderAssignment",
                newName: "IX_WorkOrderAssignment_AccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransitions_WorkflowId_TransitionId",
                table: "WorkflowTransition",
                newName: "IX_WorkflowTransition_WorkflowId_TransitionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransitions_ToStatusId",
                table: "WorkflowTransition",
                newName: "IX_WorkflowTransition_ToStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransitions_FromStatusId",
                table: "WorkflowTransition",
                newName: "IX_WorkflowTransition_FromStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowStatuses_WorkflowId_StatusId",
                table: "WorkflowStatus",
                newName: "IX_WorkflowStatus_WorkflowId_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Workflows_Name",
                table: "Workflow",
                newName: "IX_Workflow_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Workflows_CreatedBy",
                table: "Workflow",
                newName: "IX_Workflow_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRules_WorkflowId_RuleId",
                table: "WorkflowRule",
                newName: "IX_WorkflowRule_WorkflowId_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRuleConditions_RuleId",
                table: "WorkflowRuleCondition",
                newName: "IX_WorkflowRuleCondition_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRuleActions_RuleId",
                table: "WorkflowRuleAction",
                newName: "IX_WorkflowRuleAction_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowProgresses_WorkflowId",
                table: "WorkflowProgress",
                newName: "IX_WorkflowProgress_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowProgresses_CurrentStatusId",
                table: "WorkflowProgress",
                newName: "IX_WorkflowProgress_CurrentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowNodes_WorkflowId_NodeId",
                table: "WorkflowNode",
                newName: "IX_WorkflowNode_WorkflowId_NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstanceSteps_WorkflowNodeId",
                table: "WorkflowInstanceStep",
                newName: "IX_WorkflowInstanceStep_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstanceSteps_InstanceId",
                table: "WorkflowInstanceStep",
                newName: "IX_WorkflowInstanceStep_InstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstances_WorkflowId",
                table: "WorkflowInstance",
                newName: "IX_WorkflowInstance_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstances_CreatedBy",
                table: "WorkflowInstance",
                newName: "IX_WorkflowInstance_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowForms_WorkflowId_FormId",
                table: "WorkflowForm",
                newName: "IX_WorkflowForm_WorkflowId_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowForms_FormId",
                table: "WorkflowForm",
                newName: "IX_WorkflowForm_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdges_WorkflowNodeId1",
                table: "WorkflowEdge",
                newName: "IX_WorkflowEdge_WorkflowNodeId1");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdges_WorkflowNodeId",
                table: "WorkflowEdge",
                newName: "IX_WorkflowEdge_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdges_WorkflowId_EdgeId",
                table: "WorkflowEdge",
                newName: "IX_WorkflowEdge_WorkflowId_EdgeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignments_WorkflowNodeId",
                table: "WorkflowAssignment",
                newName: "IX_WorkflowAssignment_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignments_WorkflowId",
                table: "WorkflowAssignment",
                newName: "IX_WorkflowAssignment_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignments_AssigneeId",
                table: "WorkflowAssignment",
                newName: "IX_WorkflowAssignment_AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowActions_WorkflowId_ActionId",
                table: "WorkflowAction",
                newName: "IX_WorkflowAction_WorkflowId_ActionId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_Status",
                table: "Vendor",
                newName: "IX_Vendor_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Vendors_Name",
                table: "Vendor",
                newName: "IX_Vendor_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssues_Type",
                table: "ValidationIssue",
                newName: "IX_ValidationIssue_Type");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssues_Severity",
                table: "ValidationIssue",
                newName: "IX_ValidationIssue_Severity");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssues_ResolvedByUserId",
                table: "ValidationIssue",
                newName: "IX_ValidationIssue_ResolvedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssues_IsResolved",
                table: "ValidationIssue",
                newName: "IX_ValidationIssue_IsResolved");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssues_AssetId",
                table: "ValidationIssue",
                newName: "IX_ValidationIssue_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianZones_ZoneId",
                table: "TechnicianZone",
                newName: "IX_TechnicianZone_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianZones_TechnicianId",
                table: "TechnicianZone",
                newName: "IX_TechnicianZone_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianSkills_TechnicianId_SkillId",
                table: "TechnicianSkill",
                newName: "IX_TechnicianSkill_TechnicianId_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianSkills_SkillId",
                table: "TechnicianSkill",
                newName: "IX_TechnicianSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianEquipments_TechnicianId",
                table: "TechnicianEquipment",
                newName: "IX_TechnicianEquipment_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianEquipments_EquipmentId",
                table: "TechnicianEquipment",
                newName: "IX_TechnicianEquipment_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_Name",
                table: "Skill",
                newName: "IX_Skill_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_Category",
                table: "Skill",
                newName: "IX_Skill_Category");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotifications_UserId",
                table: "SentNotification",
                newName: "IX_SentNotification_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotifications_TemplateId",
                table: "SentNotification",
                newName: "IX_SentNotification_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotifications_Status",
                table: "SentNotification",
                newName: "IX_SentNotification_Status");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotifications_Recipient",
                table: "SentNotification",
                newName: "IX_SentNotification_Recipient");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_ScanType",
                table: "ScanLog",
                newName: "IX_ScanLog_ScanType");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_ScannedCode",
                table: "ScanLog",
                newName: "IX_ScanLog_ScannedCode");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_ScannedByUserId",
                table: "ScanLog",
                newName: "IX_ScanLog_ScannedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_Result",
                table: "ScanLog",
                newName: "IX_ScanLog_Result");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_InventoryId",
                table: "ScanLog",
                newName: "IX_ScanLog_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLogs_AssetId",
                table: "ScanLog",
                newName: "IX_ScanLog_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteStops_WorkOrderId",
                table: "RouteStop",
                newName: "IX_RouteStop_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteStops_RouteId_SequenceNumber",
                table: "RouteStop",
                newName: "IX_RouteStop_RouteId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_Status",
                table: "Route",
                newName: "IX_Route_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_RouteDate",
                table: "Route",
                newName: "IX_Route_RouteDate");

            migrationBuilder.RenameIndex(
                name: "IX_Routes_DriverId",
                table: "Route",
                newName: "IX_Route_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplates_UserId",
                table: "NotificationTemplate",
                newName: "IX_NotificationTemplate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplates_Type",
                table: "NotificationTemplate",
                newName: "IX_NotificationTemplate_Type");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplates_Name",
                table: "NotificationTemplate",
                newName: "IX_NotificationTemplate_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_Type",
                table: "Location",
                newName: "IX_Location_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_Status",
                table: "Location",
                newName: "IX_Location_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Location",
                newName: "IX_Location_ParentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_ManagerId",
                table: "Location",
                newName: "IX_Location_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_Code",
                table: "Location",
                newName: "IX_Location_Code");

            migrationBuilder.RenameIndex(
                name: "IX_JobKits_Status",
                table: "JobKit",
                newName: "IX_JobKit_Status");

            migrationBuilder.RenameIndex(
                name: "IX_JobKits_Name",
                table: "JobKit",
                newName: "IX_JobKit_Name");

            migrationBuilder.RenameIndex(
                name: "IX_JobKits_JobType",
                table: "JobKit",
                newName: "IX_JobKit_JobType");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItems_JobKitId",
                table: "JobKitItem",
                newName: "IX_JobKitItem_JobKitId");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItems_IsOptional",
                table: "JobKitItem",
                newName: "IX_JobKitItem_IsOptional");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItems_InventoryId",
                table: "JobKitItem",
                newName: "IX_JobKitItem_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_UserId",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_TransactionType",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_TransactionType");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_TransactionDate",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_TransactionDate");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_ToLocationId",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_ToLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_Status",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_InventoryId",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransactions_FromLocationId",
                table: "InventoryTransaction",
                newName: "IX_InventoryTransaction_FromLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocations_Status",
                table: "InventoryLocation",
                newName: "IX_InventoryLocation_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocations_LocationId",
                table: "InventoryLocation",
                newName: "IX_InventoryLocation_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocations_InventoryId_LocationId",
                table: "InventoryLocation",
                newName: "IX_InventoryLocation_InventoryId_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDispositions_VendorId",
                table: "InventoryDisposition",
                newName: "IX_InventoryDisposition_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDispositions_Status",
                table: "InventoryDisposition",
                newName: "IX_InventoryDisposition_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDispositions_InventoryId",
                table: "InventoryDisposition",
                newName: "IX_InventoryDisposition_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDispositions_DispositionType",
                table: "InventoryDisposition",
                newName: "IX_InventoryDisposition_DispositionType");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDispositions_DisposedByUserId",
                table: "InventoryDisposition",
                newName: "IX_InventoryDisposition_DisposedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_UserId",
                table: "Form",
                newName: "IX_Form_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_Name",
                table: "Form",
                newName: "IX_Form_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCounts_Status",
                table: "CycleCount",
                newName: "IX_CycleCount_Status");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCounts_PlannedDate",
                table: "CycleCount",
                newName: "IX_CycleCount_PlannedDate");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCounts_LocationId",
                table: "CycleCount",
                newName: "IX_CycleCount_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCounts_CountedByUserId",
                table: "CycleCount",
                newName: "IX_CycleCount_CountedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCountItems_InventoryId",
                table: "CycleCountItem",
                newName: "IX_CycleCountItem_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCountItems_CycleCountId",
                table: "CycleCountItem",
                newName: "IX_CycleCountItem_CycleCountId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateZones_ZoneId",
                table: "AssignmentTemplateZone",
                newName: "IX_AssignmentTemplateZone_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateZones_AssignmentTemplateId",
                table: "AssignmentTemplateZone",
                newName: "IX_AssignmentTemplateZone_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateWorkTypes_WorkOrderTypeId",
                table: "AssignmentTemplateWorkType",
                newName: "IX_AssignmentTemplateWorkType_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateWorkTypes_AssignmentTemplateId",
                table: "AssignmentTemplateWorkType",
                newName: "IX_AssignmentTemplateWorkType_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateTechnicians_TechnicianId",
                table: "AssignmentTemplateTechnician",
                newName: "IX_AssignmentTemplateTechnician_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateTechnicians_AssignmentTemplateId",
                table: "AssignmentTemplateTechnician",
                newName: "IX_AssignmentTemplateTechnician_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateSkills_SkillId",
                table: "AssignmentTemplateSkill",
                newName: "IX_AssignmentTemplateSkill_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateSkills_AssignmentTemplateId",
                table: "AssignmentTemplateSkill",
                newName: "IX_AssignmentTemplateSkill_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplates_Name",
                table: "AssignmentTemplate",
                newName: "IX_AssignmentTemplate_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplates_IsActive",
                table: "AssignmentTemplate",
                newName: "IX_AssignmentTemplate_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_WorkOrderId",
                table: "Asset",
                newName: "IX_Asset_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_Status",
                table: "Asset",
                newName: "IX_Asset_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_SerialNumber",
                table: "Asset",
                newName: "IX_Asset_SerialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_Model",
                table: "Asset",
                newName: "IX_Asset_Model");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_CurrentLocationId",
                table: "Asset",
                newName: "IX_Asset_CurrentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_Category",
                table: "Asset",
                newName: "IX_Asset_Category");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_WorkOrderId",
                table: "AssetHistory",
                newName: "IX_AssetHistory_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_UserId",
                table: "AssetHistory",
                newName: "IX_AssetHistory_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_ToLocationId",
                table: "AssetHistory",
                newName: "IX_AssetHistory_ToLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_Status",
                table: "AssetHistory",
                newName: "IX_AssetHistory_Status");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_FromLocationId",
                table: "AssetHistory",
                newName: "IX_AssetHistory_FromLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_AssetId",
                table: "AssetHistory",
                newName: "IX_AssetHistory_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistories_Action",
                table: "AssetHistory",
                newName: "IX_AssetHistory_Action");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zone",
                table: "Zone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderZone",
                table: "WorkOrderZone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderTypeSkillRequirement",
                table: "WorkOrderTypeSkillRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderType",
                table: "WorkOrderType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderTypeEquipmentRequirement",
                table: "WorkOrderTypeEquipmentRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderSkillRequirement",
                table: "WorkOrderSkillRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrder",
                table: "WorkOrder",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderEquipmentRequirement",
                table: "WorkOrderEquipmentRequirement",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderAttachment",
                table: "WorkOrderAttachment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderAssignment",
                table: "WorkOrderAssignment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowTransition",
                table: "WorkflowTransition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowStatus",
                table: "WorkflowStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRule",
                table: "WorkflowRule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRuleCondition",
                table: "WorkflowRuleCondition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRuleAction",
                table: "WorkflowRuleAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowProgress",
                table: "WorkflowProgress",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowNode",
                table: "WorkflowNode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowInstanceStep",
                table: "WorkflowInstanceStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowInstance",
                table: "WorkflowInstance",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowForm",
                table: "WorkflowForm",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowEdge",
                table: "WorkflowEdge",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowAssignment",
                table: "WorkflowAssignment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowAction",
                table: "WorkflowAction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationIssue",
                table: "ValidationIssue",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianZone",
                table: "TechnicianZone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianSkill",
                table: "TechnicianSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianEquipment",
                table: "TechnicianEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skill",
                table: "Skill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SentNotification",
                table: "SentNotification",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanLog",
                table: "ScanLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteStop",
                table: "RouteStop",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Route",
                table: "Route",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplate",
                table: "NotificationTemplate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKit",
                table: "JobKit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKitItem",
                table: "JobKitItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryTransaction",
                table: "InventoryTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryLocation",
                table: "InventoryLocation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryDisposition",
                table: "InventoryDisposition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Form",
                table: "Form",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CycleCount",
                table: "CycleCount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CycleCountItem",
                table: "CycleCountItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateZone",
                table: "AssignmentTemplateZone",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateWorkType",
                table: "AssignmentTemplateWorkType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateTechnician",
                table: "AssignmentTemplateTechnician",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateSkill",
                table: "AssignmentTemplateSkill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplate",
                table: "AssignmentTemplate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Asset",
                table: "Asset",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetHistory",
                table: "AssetHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Location_CurrentLocationId",
                table: "Asset",
                column: "CurrentLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_WorkOrder_WorkOrderId",
                table: "Asset",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistory_AspNetUsers_UserId",
                table: "AssetHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistory_Asset_AssetId",
                table: "AssetHistory",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistory_Location_FromLocationId",
                table: "AssetHistory",
                column: "FromLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistory_Location_ToLocationId",
                table: "AssetHistory",
                column: "ToLocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistory_WorkOrder_WorkOrderId",
                table: "AssetHistory",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateSkill_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateSkill",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateSkill_Skill_SkillId",
                table: "AssignmentTemplateSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateTechnician_AspNetUsers_TechnicianId",
                table: "AssignmentTemplateTechnician",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateTechnician_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateTechnician",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateWorkType_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateWorkType",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateWorkType_WorkOrderType_WorkOrderTypeId",
                table: "AssignmentTemplateWorkType",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateZone_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateZone",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateZone_Zone_ZoneId",
                table: "AssignmentTemplateZone",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCount_AspNetUsers_CountedByUserId",
                table: "CycleCount",
                column: "CountedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCount_Location_LocationId",
                table: "CycleCount",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCountItem_CycleCount_CycleCountId",
                table: "CycleCountItem",
                column: "CycleCountId",
                principalTable: "CycleCount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCountItem_Inventory_InventoryId",
                table: "CycleCountItem",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_AspNetUsers_UserId",
                table: "Form",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDisposition_AspNetUsers_DisposedByUserId",
                table: "InventoryDisposition",
                column: "DisposedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDisposition_Inventory_InventoryId",
                table: "InventoryDisposition",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDisposition_Vendor_VendorId",
                table: "InventoryDisposition",
                column: "VendorId",
                principalTable: "Vendor",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLocation_Inventory_InventoryId",
                table: "InventoryLocation",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLocation_Location_LocationId",
                table: "InventoryLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_AspNetUsers_UserId",
                table: "InventoryTransaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_Inventory_InventoryId",
                table: "InventoryTransaction",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_Location_FromLocationId",
                table: "InventoryTransaction",
                column: "FromLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransaction_Location_ToLocationId",
                table: "InventoryTransaction",
                column: "ToLocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobKitItem_Inventory_InventoryId",
                table: "JobKitItem",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobKitItem_JobKit_JobKitId",
                table: "JobKitItem",
                column: "JobKitId",
                principalTable: "JobKit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_AspNetUsers_ManagerId",
                table: "Location",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Location",
                column: "ParentLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplate_AspNetUsers_UserId",
                table: "NotificationTemplate",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Route_AspNetUsers_DriverId",
                table: "Route",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStop_WorkOrder_WorkOrderId",
                table: "RouteStop",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLog_AspNetUsers_ScannedByUserId",
                table: "ScanLog",
                column: "ScannedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLog_Asset_AssetId",
                table: "ScanLog",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLog_Inventory_InventoryId",
                table: "ScanLog",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SentNotification_AspNetUsers_UserId",
                table: "SentNotification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SentNotification_NotificationTemplate_TemplateId",
                table: "SentNotification",
                column: "TemplateId",
                principalTable: "NotificationTemplate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianEquipment_AspNetUsers_TechnicianId",
                table: "TechnicianEquipment",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianEquipment_Equipment_EquipmentId",
                table: "TechnicianEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianSkill_AspNetUsers_TechnicianId",
                table: "TechnicianSkill",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianSkill_Skill_SkillId",
                table: "TechnicianSkill",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianZone_AspNetUsers_TechnicianId",
                table: "TechnicianZone",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianZone_Zone_ZoneId",
                table: "TechnicianZone",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationIssue_AspNetUsers_ResolvedByUserId",
                table: "ValidationIssue",
                column: "ResolvedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationIssue_Asset_AssetId",
                table: "ValidationIssue",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflow_AspNetUsers_CreatedBy",
                table: "Workflow",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAction_Workflow_WorkflowId",
                table: "WorkflowAction",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignment_AspNetUsers_AssigneeId",
                table: "WorkflowAssignment",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignment_WorkflowNode_WorkflowNodeId",
                table: "WorkflowAssignment",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignment_Workflow_WorkflowId",
                table: "WorkflowAssignment",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdge_WorkflowNode_WorkflowNodeId",
                table: "WorkflowEdge",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdge_WorkflowNode_WorkflowNodeId1",
                table: "WorkflowEdge",
                column: "WorkflowNodeId1",
                principalTable: "WorkflowNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdge_Workflow_WorkflowId",
                table: "WorkflowEdge",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowForm_Form_FormId",
                table: "WorkflowForm",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowForm_Workflow_WorkflowId",
                table: "WorkflowForm",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstance_AspNetUsers_CreatedBy",
                table: "WorkflowInstance",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstance_Workflow_WorkflowId",
                table: "WorkflowInstance",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstanceStep_WorkflowInstance_InstanceId",
                table: "WorkflowInstanceStep",
                column: "InstanceId",
                principalTable: "WorkflowInstance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstanceStep_WorkflowNode_WorkflowNodeId",
                table: "WorkflowInstanceStep",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowNode_Workflow_WorkflowId",
                table: "WorkflowNode",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowProgress_WorkflowStatus_CurrentStatusId",
                table: "WorkflowProgress",
                column: "CurrentStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowProgress_Workflow_WorkflowId",
                table: "WorkflowProgress",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRule_Workflow_WorkflowId",
                table: "WorkflowRule",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRuleAction_WorkflowRule_RuleId",
                table: "WorkflowRuleAction",
                column: "RuleId",
                principalTable: "WorkflowRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRuleCondition_WorkflowRule_RuleId",
                table: "WorkflowRuleCondition",
                column: "RuleId",
                principalTable: "WorkflowRule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransition_WorkflowStatus_FromStatusId",
                table: "WorkflowTransition",
                column: "FromStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransition_WorkflowStatus_ToStatusId",
                table: "WorkflowTransition",
                column: "ToStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransition_Workflow_WorkflowId",
                table: "WorkflowTransition",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_AspNetUsers_AssignedTechnicianId",
                table: "WorkOrder",
                column: "AssignedTechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_AspNetUsers_CreatedBy",
                table: "WorkOrder",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrder",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_WorkflowStatus_WorkflowStatusId",
                table: "WorkOrder",
                column: "WorkflowStatusId",
                principalTable: "WorkflowStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrder_Workflow_WorkflowId",
                table: "WorkOrder",
                column: "WorkflowId",
                principalTable: "Workflow",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAssignment_WorkOrder_WorkOrderId",
                table: "WorkOrderAssignment",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAttachment_AspNetUsers_UploadedBy",
                table: "WorkOrderAttachment",
                column: "UploadedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAttachment_WorkOrder_WorkOrderId",
                table: "WorkOrderAttachment",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEquipmentRequirement_Equipment_EquipmentId",
                table: "WorkOrderEquipmentRequirement",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEquipmentRequirement_WorkOrder_WorkOrderId",
                table: "WorkOrderEquipmentRequirement",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderSkillRequirement_Skill_SkillId",
                table: "WorkOrderSkillRequirement",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderSkillRequirement_WorkOrder_WorkOrderId",
                table: "WorkOrderSkillRequirement",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirement_Equipment_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirement",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirement_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirement",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeSkillRequirement_Skill_SkillId",
                table: "WorkOrderTypeSkillRequirement",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeSkillRequirement_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirement",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderZone_WorkOrder_WorkOrderId",
                table: "WorkOrderZone",
                column: "WorkOrderId",
                principalTable: "WorkOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderZone_Zone_ZoneId",
                table: "WorkOrderZone",
                column: "ZoneId",
                principalTable: "Zone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Location_CurrentLocationId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_Asset_WorkOrder_WorkOrderId",
                table: "Asset");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistory_AspNetUsers_UserId",
                table: "AssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistory_Asset_AssetId",
                table: "AssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistory_Location_FromLocationId",
                table: "AssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistory_Location_ToLocationId",
                table: "AssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetHistory_WorkOrder_WorkOrderId",
                table: "AssetHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateSkill_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateSkill_Skill_SkillId",
                table: "AssignmentTemplateSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateTechnician_AspNetUsers_TechnicianId",
                table: "AssignmentTemplateTechnician");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateTechnician_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateTechnician");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateWorkType_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateWorkType");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateWorkType_WorkOrderType_WorkOrderTypeId",
                table: "AssignmentTemplateWorkType");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateZone_AssignmentTemplate_AssignmentTemplateId",
                table: "AssignmentTemplateZone");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentTemplateZone_Zone_ZoneId",
                table: "AssignmentTemplateZone");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCount_AspNetUsers_CountedByUserId",
                table: "CycleCount");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCount_Location_LocationId",
                table: "CycleCount");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCountItem_CycleCount_CycleCountId",
                table: "CycleCountItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CycleCountItem_Inventory_InventoryId",
                table: "CycleCountItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_AspNetUsers_UserId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDisposition_AspNetUsers_DisposedByUserId",
                table: "InventoryDisposition");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDisposition_Inventory_InventoryId",
                table: "InventoryDisposition");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryDisposition_Vendor_VendorId",
                table: "InventoryDisposition");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLocation_Inventory_InventoryId",
                table: "InventoryLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLocation_Location_LocationId",
                table: "InventoryLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_AspNetUsers_UserId",
                table: "InventoryTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_Inventory_InventoryId",
                table: "InventoryTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_Location_FromLocationId",
                table: "InventoryTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryTransaction_Location_ToLocationId",
                table: "InventoryTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKitItem_Inventory_InventoryId",
                table: "JobKitItem");

            migrationBuilder.DropForeignKey(
                name: "FK_JobKitItem_JobKit_JobKitId",
                table: "JobKitItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_AspNetUsers_ManagerId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Location_ParentLocationId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplate_AspNetUsers_UserId",
                table: "NotificationTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropForeignKey(
                name: "FK_Route_AspNetUsers_DriverId",
                table: "Route");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_Route_RouteId",
                table: "RouteStop");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteStop_WorkOrder_WorkOrderId",
                table: "RouteStop");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLog_AspNetUsers_ScannedByUserId",
                table: "ScanLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLog_Asset_AssetId",
                table: "ScanLog");

            migrationBuilder.DropForeignKey(
                name: "FK_ScanLog_Inventory_InventoryId",
                table: "ScanLog");

            migrationBuilder.DropForeignKey(
                name: "FK_SentNotification_AspNetUsers_UserId",
                table: "SentNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_SentNotification_NotificationTemplate_TemplateId",
                table: "SentNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianEquipment_AspNetUsers_TechnicianId",
                table: "TechnicianEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianEquipment_Equipment_EquipmentId",
                table: "TechnicianEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianSkill_AspNetUsers_TechnicianId",
                table: "TechnicianSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianSkill_Skill_SkillId",
                table: "TechnicianSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianZone_AspNetUsers_TechnicianId",
                table: "TechnicianZone");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicianZone_Zone_ZoneId",
                table: "TechnicianZone");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationIssue_AspNetUsers_ResolvedByUserId",
                table: "ValidationIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationIssue_Asset_AssetId",
                table: "ValidationIssue");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflow_AspNetUsers_CreatedBy",
                table: "Workflow");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAction_Workflow_WorkflowId",
                table: "WorkflowAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignment_AspNetUsers_AssigneeId",
                table: "WorkflowAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignment_WorkflowNode_WorkflowNodeId",
                table: "WorkflowAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowAssignment_Workflow_WorkflowId",
                table: "WorkflowAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdge_WorkflowNode_WorkflowNodeId",
                table: "WorkflowEdge");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdge_WorkflowNode_WorkflowNodeId1",
                table: "WorkflowEdge");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowEdge_Workflow_WorkflowId",
                table: "WorkflowEdge");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowForm_Form_FormId",
                table: "WorkflowForm");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowForm_Workflow_WorkflowId",
                table: "WorkflowForm");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstance_AspNetUsers_CreatedBy",
                table: "WorkflowInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstance_Workflow_WorkflowId",
                table: "WorkflowInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstanceStep_WorkflowInstance_InstanceId",
                table: "WorkflowInstanceStep");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowInstanceStep_WorkflowNode_WorkflowNodeId",
                table: "WorkflowInstanceStep");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowNode_Workflow_WorkflowId",
                table: "WorkflowNode");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowProgress_WorkflowStatus_CurrentStatusId",
                table: "WorkflowProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowProgress_Workflow_WorkflowId",
                table: "WorkflowProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRule_Workflow_WorkflowId",
                table: "WorkflowRule");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRuleAction_WorkflowRule_RuleId",
                table: "WorkflowRuleAction");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowRuleCondition_WorkflowRule_RuleId",
                table: "WorkflowRuleCondition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowStatus_Workflow_WorkflowId",
                table: "WorkflowStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransition_WorkflowStatus_FromStatusId",
                table: "WorkflowTransition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransition_WorkflowStatus_ToStatusId",
                table: "WorkflowTransition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkflowTransition_Workflow_WorkflowId",
                table: "WorkflowTransition");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_AspNetUsers_AssignedTechnicianId",
                table: "WorkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_AspNetUsers_CreatedBy",
                table: "WorkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_WorkflowStatus_WorkflowStatusId",
                table: "WorkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrder_Workflow_WorkflowId",
                table: "WorkOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAssignment_WorkOrder_WorkOrderId",
                table: "WorkOrderAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAttachment_AspNetUsers_UploadedBy",
                table: "WorkOrderAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderAttachment_WorkOrder_WorkOrderId",
                table: "WorkOrderAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEquipmentRequirement_Equipment_EquipmentId",
                table: "WorkOrderEquipmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderEquipmentRequirement_WorkOrder_WorkOrderId",
                table: "WorkOrderEquipmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderSkillRequirement_Skill_SkillId",
                table: "WorkOrderSkillRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderSkillRequirement_WorkOrder_WorkOrderId",
                table: "WorkOrderSkillRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirement_Equipment_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirement_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeSkillRequirement_Skill_SkillId",
                table: "WorkOrderTypeSkillRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderTypeSkillRequirement_WorkOrderType_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderZone_WorkOrder_WorkOrderId",
                table: "WorkOrderZone");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrderZone_Zone_ZoneId",
                table: "WorkOrderZone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Zone",
                table: "Zone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderZone",
                table: "WorkOrderZone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderTypeSkillRequirement",
                table: "WorkOrderTypeSkillRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderTypeEquipmentRequirement",
                table: "WorkOrderTypeEquipmentRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderType",
                table: "WorkOrderType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderSkillRequirement",
                table: "WorkOrderSkillRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderEquipmentRequirement",
                table: "WorkOrderEquipmentRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderAttachment",
                table: "WorkOrderAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrderAssignment",
                table: "WorkOrderAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkOrder",
                table: "WorkOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowTransition",
                table: "WorkflowTransition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowStatus",
                table: "WorkflowStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRuleCondition",
                table: "WorkflowRuleCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRuleAction",
                table: "WorkflowRuleAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowRule",
                table: "WorkflowRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowProgress",
                table: "WorkflowProgress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowNode",
                table: "WorkflowNode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowInstanceStep",
                table: "WorkflowInstanceStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowInstance",
                table: "WorkflowInstance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowForm",
                table: "WorkflowForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowEdge",
                table: "WorkflowEdge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowAssignment",
                table: "WorkflowAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkflowAction",
                table: "WorkflowAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Workflow",
                table: "Workflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vendor",
                table: "Vendor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationIssue",
                table: "ValidationIssue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianZone",
                table: "TechnicianZone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianSkill",
                table: "TechnicianSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicianEquipment",
                table: "TechnicianEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skill",
                table: "Skill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SentNotification",
                table: "SentNotification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScanLog",
                table: "ScanLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteStop",
                table: "RouteStop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Route",
                table: "Route");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationTemplate",
                table: "NotificationTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKitItem",
                table: "JobKitItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobKit",
                table: "JobKit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryTransaction",
                table: "InventoryTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryLocation",
                table: "InventoryLocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryDisposition",
                table: "InventoryDisposition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Form",
                table: "Form");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CycleCountItem",
                table: "CycleCountItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CycleCount",
                table: "CycleCount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateZone",
                table: "AssignmentTemplateZone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateWorkType",
                table: "AssignmentTemplateWorkType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateTechnician",
                table: "AssignmentTemplateTechnician");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplateSkill",
                table: "AssignmentTemplateSkill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentTemplate",
                table: "AssignmentTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssetHistory",
                table: "AssetHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Asset",
                table: "Asset");

            migrationBuilder.RenameTable(
                name: "Zone",
                newName: "Zones");

            migrationBuilder.RenameTable(
                name: "WorkOrderZone",
                newName: "WorkOrderZones");

            migrationBuilder.RenameTable(
                name: "WorkOrderTypeSkillRequirement",
                newName: "WorkOrderTypeSkillRequirements");

            migrationBuilder.RenameTable(
                name: "WorkOrderTypeEquipmentRequirement",
                newName: "WorkOrderTypeEquipmentRequirements");

            migrationBuilder.RenameTable(
                name: "WorkOrderType",
                newName: "WorkOrderTypes");

            migrationBuilder.RenameTable(
                name: "WorkOrderSkillRequirement",
                newName: "WorkOrderSkillRequirements");

            migrationBuilder.RenameTable(
                name: "WorkOrderEquipmentRequirement",
                newName: "WorkOrderEquipmentRequirements");

            migrationBuilder.RenameTable(
                name: "WorkOrderAttachment",
                newName: "WorkOrderAttachments");

            migrationBuilder.RenameTable(
                name: "WorkOrderAssignment",
                newName: "WorkOrderAssignments");

            migrationBuilder.RenameTable(
                name: "WorkOrder",
                newName: "WorkOrders");

            migrationBuilder.RenameTable(
                name: "WorkflowTransition",
                newName: "WorkflowTransitions");

            migrationBuilder.RenameTable(
                name: "WorkflowStatus",
                newName: "WorkflowStatuses");

            migrationBuilder.RenameTable(
                name: "WorkflowRuleCondition",
                newName: "WorkflowRuleConditions");

            migrationBuilder.RenameTable(
                name: "WorkflowRuleAction",
                newName: "WorkflowRuleActions");

            migrationBuilder.RenameTable(
                name: "WorkflowRule",
                newName: "WorkflowRules");

            migrationBuilder.RenameTable(
                name: "WorkflowProgress",
                newName: "WorkflowProgresses");

            migrationBuilder.RenameTable(
                name: "WorkflowNode",
                newName: "WorkflowNodes");

            migrationBuilder.RenameTable(
                name: "WorkflowInstanceStep",
                newName: "WorkflowInstanceSteps");

            migrationBuilder.RenameTable(
                name: "WorkflowInstance",
                newName: "WorkflowInstances");

            migrationBuilder.RenameTable(
                name: "WorkflowForm",
                newName: "WorkflowForms");

            migrationBuilder.RenameTable(
                name: "WorkflowEdge",
                newName: "WorkflowEdges");

            migrationBuilder.RenameTable(
                name: "WorkflowAssignment",
                newName: "WorkflowAssignments");

            migrationBuilder.RenameTable(
                name: "WorkflowAction",
                newName: "WorkflowActions");

            migrationBuilder.RenameTable(
                name: "Workflow",
                newName: "Workflows");

            migrationBuilder.RenameTable(
                name: "Vendor",
                newName: "Vendors");

            migrationBuilder.RenameTable(
                name: "ValidationIssue",
                newName: "ValidationIssues");

            migrationBuilder.RenameTable(
                name: "TechnicianZone",
                newName: "TechnicianZones");

            migrationBuilder.RenameTable(
                name: "TechnicianSkill",
                newName: "TechnicianSkills");

            migrationBuilder.RenameTable(
                name: "TechnicianEquipment",
                newName: "TechnicianEquipments");

            migrationBuilder.RenameTable(
                name: "Skill",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "SentNotification",
                newName: "SentNotifications");

            migrationBuilder.RenameTable(
                name: "ScanLog",
                newName: "ScanLogs");

            migrationBuilder.RenameTable(
                name: "RouteStop",
                newName: "RouteStops");

            migrationBuilder.RenameTable(
                name: "Route",
                newName: "Routes");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "NotificationTemplate",
                newName: "NotificationTemplates");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "JobKitItem",
                newName: "JobKitItems");

            migrationBuilder.RenameTable(
                name: "JobKit",
                newName: "JobKits");

            migrationBuilder.RenameTable(
                name: "InventoryTransaction",
                newName: "InventoryTransactions");

            migrationBuilder.RenameTable(
                name: "InventoryLocation",
                newName: "InventoryLocations");

            migrationBuilder.RenameTable(
                name: "InventoryDisposition",
                newName: "InventoryDispositions");

            migrationBuilder.RenameTable(
                name: "Form",
                newName: "Forms");

            migrationBuilder.RenameTable(
                name: "CycleCountItem",
                newName: "CycleCountItems");

            migrationBuilder.RenameTable(
                name: "CycleCount",
                newName: "CycleCounts");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateZone",
                newName: "AssignmentTemplateZones");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateWorkType",
                newName: "AssignmentTemplateWorkTypes");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateTechnician",
                newName: "AssignmentTemplateTechnicians");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplateSkill",
                newName: "AssignmentTemplateSkills");

            migrationBuilder.RenameTable(
                name: "AssignmentTemplate",
                newName: "AssignmentTemplates");

            migrationBuilder.RenameTable(
                name: "AssetHistory",
                newName: "AssetHistories");

            migrationBuilder.RenameTable(
                name: "Asset",
                newName: "Assets");

            migrationBuilder.RenameIndex(
                name: "IX_Zone_Type",
                table: "Zones",
                newName: "IX_Zones_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Zone_Name",
                table: "Zones",
                newName: "IX_Zones_Name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderZone_ZoneId",
                table: "WorkOrderZones",
                newName: "IX_WorkOrderZones_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderZone_WorkOrderId",
                table: "WorkOrderZones",
                newName: "IX_WorkOrderZones_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeSkillRequirement_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirements",
                newName: "IX_WorkOrderTypeSkillRequirements_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeSkillRequirement_SkillId",
                table: "WorkOrderTypeSkillRequirements",
                newName: "IX_WorkOrderTypeSkillRequirements_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeEquipmentRequirement_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirements",
                newName: "IX_WorkOrderTypeEquipmentRequirements_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderTypeEquipmentRequirement_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirements",
                newName: "IX_WorkOrderTypeEquipmentRequirements_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderType_Name",
                table: "WorkOrderTypes",
                newName: "IX_WorkOrderTypes_Name");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderType_Category",
                table: "WorkOrderTypes",
                newName: "IX_WorkOrderTypes_Category");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderSkillRequirement_WorkOrderId",
                table: "WorkOrderSkillRequirements",
                newName: "IX_WorkOrderSkillRequirements_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderSkillRequirement_SkillId",
                table: "WorkOrderSkillRequirements",
                newName: "IX_WorkOrderSkillRequirements_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderEquipmentRequirement_WorkOrderId",
                table: "WorkOrderEquipmentRequirements",
                newName: "IX_WorkOrderEquipmentRequirements_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderEquipmentRequirement_EquipmentId",
                table: "WorkOrderEquipmentRequirements",
                newName: "IX_WorkOrderEquipmentRequirements_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachment_WorkOrderId",
                table: "WorkOrderAttachments",
                newName: "IX_WorkOrderAttachments_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachment_UploadedBy",
                table: "WorkOrderAttachments",
                newName: "IX_WorkOrderAttachments_UploadedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAttachment_FileName",
                table: "WorkOrderAttachments",
                newName: "IX_WorkOrderAttachments_FileName");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignment_WorkOrderId",
                table: "WorkOrderAssignments",
                newName: "IX_WorkOrderAssignments_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignment_Identifier",
                table: "WorkOrderAssignments",
                newName: "IX_WorkOrderAssignments_Identifier");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrderAssignment_AccountNumber",
                table: "WorkOrderAssignments",
                newName: "IX_WorkOrderAssignments_AccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_WorkOrderTypeId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_WorkOrderNumber",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkOrderNumber");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_WorkflowStatusId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkflowStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_WorkflowId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_Status",
                table: "WorkOrders",
                newName: "IX_WorkOrders_Status");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_Priority",
                table: "WorkOrders",
                newName: "IX_WorkOrders_Priority");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_CreatedBy",
                table: "WorkOrders",
                newName: "IX_WorkOrders_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrder_AssignedTechnicianId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_AssignedTechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransition_WorkflowId_TransitionId",
                table: "WorkflowTransitions",
                newName: "IX_WorkflowTransitions_WorkflowId_TransitionId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransition_ToStatusId",
                table: "WorkflowTransitions",
                newName: "IX_WorkflowTransitions_ToStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowTransition_FromStatusId",
                table: "WorkflowTransitions",
                newName: "IX_WorkflowTransitions_FromStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowStatus_WorkflowId_StatusId",
                table: "WorkflowStatuses",
                newName: "IX_WorkflowStatuses_WorkflowId_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRuleCondition_RuleId",
                table: "WorkflowRuleConditions",
                newName: "IX_WorkflowRuleConditions_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRuleAction_RuleId",
                table: "WorkflowRuleActions",
                newName: "IX_WorkflowRuleActions_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowRule_WorkflowId_RuleId",
                table: "WorkflowRules",
                newName: "IX_WorkflowRules_WorkflowId_RuleId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowProgress_WorkflowId",
                table: "WorkflowProgresses",
                newName: "IX_WorkflowProgresses_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowProgress_CurrentStatusId",
                table: "WorkflowProgresses",
                newName: "IX_WorkflowProgresses_CurrentStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowNode_WorkflowId_NodeId",
                table: "WorkflowNodes",
                newName: "IX_WorkflowNodes_WorkflowId_NodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstanceStep_WorkflowNodeId",
                table: "WorkflowInstanceSteps",
                newName: "IX_WorkflowInstanceSteps_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstanceStep_InstanceId",
                table: "WorkflowInstanceSteps",
                newName: "IX_WorkflowInstanceSteps_InstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstance_WorkflowId",
                table: "WorkflowInstances",
                newName: "IX_WorkflowInstances_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowInstance_CreatedBy",
                table: "WorkflowInstances",
                newName: "IX_WorkflowInstances_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowForm_WorkflowId_FormId",
                table: "WorkflowForms",
                newName: "IX_WorkflowForms_WorkflowId_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowForm_FormId",
                table: "WorkflowForms",
                newName: "IX_WorkflowForms_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdge_WorkflowNodeId1",
                table: "WorkflowEdges",
                newName: "IX_WorkflowEdges_WorkflowNodeId1");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdge_WorkflowNodeId",
                table: "WorkflowEdges",
                newName: "IX_WorkflowEdges_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowEdge_WorkflowId_EdgeId",
                table: "WorkflowEdges",
                newName: "IX_WorkflowEdges_WorkflowId_EdgeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignment_WorkflowNodeId",
                table: "WorkflowAssignments",
                newName: "IX_WorkflowAssignments_WorkflowNodeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignment_WorkflowId",
                table: "WorkflowAssignments",
                newName: "IX_WorkflowAssignments_WorkflowId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAssignment_AssigneeId",
                table: "WorkflowAssignments",
                newName: "IX_WorkflowAssignments_AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkflowAction_WorkflowId_ActionId",
                table: "WorkflowActions",
                newName: "IX_WorkflowActions_WorkflowId_ActionId");

            migrationBuilder.RenameIndex(
                name: "IX_Workflow_Name",
                table: "Workflows",
                newName: "IX_Workflows_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Workflow_CreatedBy",
                table: "Workflows",
                newName: "IX_Workflows_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Vendor_Status",
                table: "Vendors",
                newName: "IX_Vendors_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Vendor_Name",
                table: "Vendors",
                newName: "IX_Vendors_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssue_Type",
                table: "ValidationIssues",
                newName: "IX_ValidationIssues_Type");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssue_Severity",
                table: "ValidationIssues",
                newName: "IX_ValidationIssues_Severity");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssue_ResolvedByUserId",
                table: "ValidationIssues",
                newName: "IX_ValidationIssues_ResolvedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssue_IsResolved",
                table: "ValidationIssues",
                newName: "IX_ValidationIssues_IsResolved");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationIssue_AssetId",
                table: "ValidationIssues",
                newName: "IX_ValidationIssues_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianZone_ZoneId",
                table: "TechnicianZones",
                newName: "IX_TechnicianZones_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianZone_TechnicianId",
                table: "TechnicianZones",
                newName: "IX_TechnicianZones_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianSkill_TechnicianId_SkillId",
                table: "TechnicianSkills",
                newName: "IX_TechnicianSkills_TechnicianId_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianSkill_SkillId",
                table: "TechnicianSkills",
                newName: "IX_TechnicianSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianEquipment_TechnicianId",
                table: "TechnicianEquipments",
                newName: "IX_TechnicianEquipments_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_TechnicianEquipment_EquipmentId",
                table: "TechnicianEquipments",
                newName: "IX_TechnicianEquipments_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Skill_Name",
                table: "Skills",
                newName: "IX_Skills_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Skill_Category",
                table: "Skills",
                newName: "IX_Skills_Category");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotification_UserId",
                table: "SentNotifications",
                newName: "IX_SentNotifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotification_TemplateId",
                table: "SentNotifications",
                newName: "IX_SentNotifications_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotification_Status",
                table: "SentNotifications",
                newName: "IX_SentNotifications_Status");

            migrationBuilder.RenameIndex(
                name: "IX_SentNotification_Recipient",
                table: "SentNotifications",
                newName: "IX_SentNotifications_Recipient");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_ScanType",
                table: "ScanLogs",
                newName: "IX_ScanLogs_ScanType");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_ScannedCode",
                table: "ScanLogs",
                newName: "IX_ScanLogs_ScannedCode");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_ScannedByUserId",
                table: "ScanLogs",
                newName: "IX_ScanLogs_ScannedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_Result",
                table: "ScanLogs",
                newName: "IX_ScanLogs_Result");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_InventoryId",
                table: "ScanLogs",
                newName: "IX_ScanLogs_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ScanLog_AssetId",
                table: "ScanLogs",
                newName: "IX_ScanLogs_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteStop_WorkOrderId",
                table: "RouteStops",
                newName: "IX_RouteStops_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteStop_RouteId_SequenceNumber",
                table: "RouteStops",
                newName: "IX_RouteStops_RouteId_SequenceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Route_Status",
                table: "Routes",
                newName: "IX_Routes_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Route_RouteDate",
                table: "Routes",
                newName: "IX_Routes_RouteDate");

            migrationBuilder.RenameIndex(
                name: "IX_Route_DriverId",
                table: "Routes",
                newName: "IX_Routes_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplate_UserId",
                table: "NotificationTemplates",
                newName: "IX_NotificationTemplates_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplate_Type",
                table: "NotificationTemplates",
                newName: "IX_NotificationTemplates_Type");

            migrationBuilder.RenameIndex(
                name: "IX_NotificationTemplate_Name",
                table: "NotificationTemplates",
                newName: "IX_NotificationTemplates_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Location_Type",
                table: "Locations",
                newName: "IX_Locations_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Location_Status",
                table: "Locations",
                newName: "IX_Locations_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Location_ParentLocationId",
                table: "Locations",
                newName: "IX_Locations_ParentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_ManagerId",
                table: "Locations",
                newName: "IX_Locations_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_Code",
                table: "Locations",
                newName: "IX_Locations_Code");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItem_JobKitId",
                table: "JobKitItems",
                newName: "IX_JobKitItems_JobKitId");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItem_IsOptional",
                table: "JobKitItems",
                newName: "IX_JobKitItems_IsOptional");

            migrationBuilder.RenameIndex(
                name: "IX_JobKitItem_InventoryId",
                table: "JobKitItems",
                newName: "IX_JobKitItems_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_JobKit_Status",
                table: "JobKits",
                newName: "IX_JobKits_Status");

            migrationBuilder.RenameIndex(
                name: "IX_JobKit_Name",
                table: "JobKits",
                newName: "IX_JobKits_Name");

            migrationBuilder.RenameIndex(
                name: "IX_JobKit_JobType",
                table: "JobKits",
                newName: "IX_JobKits_JobType");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_UserId",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_TransactionType",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_TransactionType");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_TransactionDate",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_TransactionDate");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_ToLocationId",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_ToLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_Status",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_InventoryId",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryTransaction_FromLocationId",
                table: "InventoryTransactions",
                newName: "IX_InventoryTransactions_FromLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocation_Status",
                table: "InventoryLocations",
                newName: "IX_InventoryLocations_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocation_LocationId",
                table: "InventoryLocations",
                newName: "IX_InventoryLocations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryLocation_InventoryId_LocationId",
                table: "InventoryLocations",
                newName: "IX_InventoryLocations_InventoryId_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDisposition_VendorId",
                table: "InventoryDispositions",
                newName: "IX_InventoryDispositions_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDisposition_Status",
                table: "InventoryDispositions",
                newName: "IX_InventoryDispositions_Status");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDisposition_InventoryId",
                table: "InventoryDispositions",
                newName: "IX_InventoryDispositions_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDisposition_DispositionType",
                table: "InventoryDispositions",
                newName: "IX_InventoryDispositions_DispositionType");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryDisposition_DisposedByUserId",
                table: "InventoryDispositions",
                newName: "IX_InventoryDispositions_DisposedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Form_UserId",
                table: "Forms",
                newName: "IX_Forms_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Form_Name",
                table: "Forms",
                newName: "IX_Forms_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCountItem_InventoryId",
                table: "CycleCountItems",
                newName: "IX_CycleCountItems_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCountItem_CycleCountId",
                table: "CycleCountItems",
                newName: "IX_CycleCountItems_CycleCountId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCount_Status",
                table: "CycleCounts",
                newName: "IX_CycleCounts_Status");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCount_PlannedDate",
                table: "CycleCounts",
                newName: "IX_CycleCounts_PlannedDate");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCount_LocationId",
                table: "CycleCounts",
                newName: "IX_CycleCounts_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_CycleCount_CountedByUserId",
                table: "CycleCounts",
                newName: "IX_CycleCounts_CountedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateZone_ZoneId",
                table: "AssignmentTemplateZones",
                newName: "IX_AssignmentTemplateZones_ZoneId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateZone_AssignmentTemplateId",
                table: "AssignmentTemplateZones",
                newName: "IX_AssignmentTemplateZones_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateWorkType_WorkOrderTypeId",
                table: "AssignmentTemplateWorkTypes",
                newName: "IX_AssignmentTemplateWorkTypes_WorkOrderTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateWorkType_AssignmentTemplateId",
                table: "AssignmentTemplateWorkTypes",
                newName: "IX_AssignmentTemplateWorkTypes_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateTechnician_TechnicianId",
                table: "AssignmentTemplateTechnicians",
                newName: "IX_AssignmentTemplateTechnicians_TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateTechnician_AssignmentTemplateId",
                table: "AssignmentTemplateTechnicians",
                newName: "IX_AssignmentTemplateTechnicians_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateSkill_SkillId",
                table: "AssignmentTemplateSkills",
                newName: "IX_AssignmentTemplateSkills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplateSkill_AssignmentTemplateId",
                table: "AssignmentTemplateSkills",
                newName: "IX_AssignmentTemplateSkills_AssignmentTemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplate_Name",
                table: "AssignmentTemplates",
                newName: "IX_AssignmentTemplates_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentTemplate_IsActive",
                table: "AssignmentTemplates",
                newName: "IX_AssignmentTemplates_IsActive");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_WorkOrderId",
                table: "AssetHistories",
                newName: "IX_AssetHistories_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_UserId",
                table: "AssetHistories",
                newName: "IX_AssetHistories_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_ToLocationId",
                table: "AssetHistories",
                newName: "IX_AssetHistories_ToLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_Status",
                table: "AssetHistories",
                newName: "IX_AssetHistories_Status");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_FromLocationId",
                table: "AssetHistories",
                newName: "IX_AssetHistories_FromLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_AssetId",
                table: "AssetHistories",
                newName: "IX_AssetHistories_AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetHistory_Action",
                table: "AssetHistories",
                newName: "IX_AssetHistories_Action");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_WorkOrderId",
                table: "Assets",
                newName: "IX_Assets_WorkOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_Status",
                table: "Assets",
                newName: "IX_Assets_Status");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_SerialNumber",
                table: "Assets",
                newName: "IX_Assets_SerialNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_Model",
                table: "Assets",
                newName: "IX_Assets_Model");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_CurrentLocationId",
                table: "Assets",
                newName: "IX_Assets_CurrentLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Asset_Category",
                table: "Assets",
                newName: "IX_Assets_Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Zones",
                table: "Zones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderZones",
                table: "WorkOrderZones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderTypeSkillRequirements",
                table: "WorkOrderTypeSkillRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderTypeEquipmentRequirements",
                table: "WorkOrderTypeEquipmentRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderTypes",
                table: "WorkOrderTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderSkillRequirements",
                table: "WorkOrderSkillRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderEquipmentRequirements",
                table: "WorkOrderEquipmentRequirements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderAttachments",
                table: "WorkOrderAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrderAssignments",
                table: "WorkOrderAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkOrders",
                table: "WorkOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowTransitions",
                table: "WorkflowTransitions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowStatuses",
                table: "WorkflowStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRuleConditions",
                table: "WorkflowRuleConditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRuleActions",
                table: "WorkflowRuleActions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowRules",
                table: "WorkflowRules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowProgresses",
                table: "WorkflowProgresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowNodes",
                table: "WorkflowNodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowInstanceSteps",
                table: "WorkflowInstanceSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowInstances",
                table: "WorkflowInstances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowForms",
                table: "WorkflowForms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowEdges",
                table: "WorkflowEdges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowAssignments",
                table: "WorkflowAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkflowActions",
                table: "WorkflowActions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Workflows",
                table: "Workflows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vendors",
                table: "Vendors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationIssues",
                table: "ValidationIssues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianZones",
                table: "TechnicianZones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianSkills",
                table: "TechnicianSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicianEquipments",
                table: "TechnicianEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SentNotifications",
                table: "SentNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScanLogs",
                table: "ScanLogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteStops",
                table: "RouteStops",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationTemplates",
                table: "NotificationTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKitItems",
                table: "JobKitItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobKits",
                table: "JobKits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryTransactions",
                table: "InventoryTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryLocations",
                table: "InventoryLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryDispositions",
                table: "InventoryDispositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forms",
                table: "Forms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CycleCountItems",
                table: "CycleCountItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CycleCounts",
                table: "CycleCounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateZones",
                table: "AssignmentTemplateZones",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateWorkTypes",
                table: "AssignmentTemplateWorkTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateTechnicians",
                table: "AssignmentTemplateTechnicians",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplateSkills",
                table: "AssignmentTemplateSkills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentTemplates",
                table: "AssignmentTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssetHistories",
                table: "AssetHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_AspNetUsers_UserId",
                table: "AssetHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_Assets_AssetId",
                table: "AssetHistories",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_Locations_FromLocationId",
                table: "AssetHistories",
                column: "FromLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_Locations_ToLocationId",
                table: "AssetHistories",
                column: "ToLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetHistories_WorkOrders_WorkOrderId",
                table: "AssetHistories",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Locations_CurrentLocationId",
                table: "Assets",
                column: "CurrentLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_WorkOrders_WorkOrderId",
                table: "Assets",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateSkills_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateSkills",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateSkills_Skills_SkillId",
                table: "AssignmentTemplateSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateTechnicians_AspNetUsers_TechnicianId",
                table: "AssignmentTemplateTechnicians",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateTechnicians_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateTechnicians",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateWorkTypes_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateWorkTypes",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateWorkTypes_WorkOrderTypes_WorkOrderTypeId",
                table: "AssignmentTemplateWorkTypes",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateZones_AssignmentTemplates_AssignmentTemplateId",
                table: "AssignmentTemplateZones",
                column: "AssignmentTemplateId",
                principalTable: "AssignmentTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentTemplateZones_Zones_ZoneId",
                table: "AssignmentTemplateZones",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCountItems_CycleCounts_CycleCountId",
                table: "CycleCountItems",
                column: "CycleCountId",
                principalTable: "CycleCounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCountItems_Inventory_InventoryId",
                table: "CycleCountItems",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCounts_AspNetUsers_CountedByUserId",
                table: "CycleCounts",
                column: "CountedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CycleCounts_Locations_LocationId",
                table: "CycleCounts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_AspNetUsers_UserId",
                table: "Forms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispositions_AspNetUsers_DisposedByUserId",
                table: "InventoryDispositions",
                column: "DisposedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispositions_Inventory_InventoryId",
                table: "InventoryDispositions",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryDispositions_Vendors_VendorId",
                table: "InventoryDispositions",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLocations_Inventory_InventoryId",
                table: "InventoryLocations",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLocations_Locations_LocationId",
                table: "InventoryLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_AspNetUsers_UserId",
                table: "InventoryTransactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Inventory_InventoryId",
                table: "InventoryTransactions",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Locations_FromLocationId",
                table: "InventoryTransactions",
                column: "FromLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryTransactions_Locations_ToLocationId",
                table: "InventoryTransactions",
                column: "ToLocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobKitItems_Inventory_InventoryId",
                table: "JobKitItems",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobKitItems_JobKits_JobKitId",
                table: "JobKitItems",
                column: "JobKitId",
                principalTable: "JobKits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_ManagerId",
                table: "Locations",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplates_AspNetUsers_UserId",
                table: "NotificationTemplates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AspNetUsers_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_AspNetUsers_DriverId",
                table: "Routes",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_Routes_RouteId",
                table: "RouteStops",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteStops_WorkOrders_WorkOrderId",
                table: "RouteStops",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLogs_AspNetUsers_ScannedByUserId",
                table: "ScanLogs",
                column: "ScannedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLogs_Assets_AssetId",
                table: "ScanLogs",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ScanLogs_Inventory_InventoryId",
                table: "ScanLogs",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SentNotifications_AspNetUsers_UserId",
                table: "SentNotifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SentNotifications_NotificationTemplates_TemplateId",
                table: "SentNotifications",
                column: "TemplateId",
                principalTable: "NotificationTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianEquipments_AspNetUsers_TechnicianId",
                table: "TechnicianEquipments",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianEquipments_Equipment_EquipmentId",
                table: "TechnicianEquipments",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianSkills_AspNetUsers_TechnicianId",
                table: "TechnicianSkills",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianSkills_Skills_SkillId",
                table: "TechnicianSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianZones_AspNetUsers_TechnicianId",
                table: "TechnicianZones",
                column: "TechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicianZones_Zones_ZoneId",
                table: "TechnicianZones",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationIssues_AspNetUsers_ResolvedByUserId",
                table: "ValidationIssues",
                column: "ResolvedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationIssues_Assets_AssetId",
                table: "ValidationIssues",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowActions_Workflows_WorkflowId",
                table: "WorkflowActions",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignments_AspNetUsers_AssigneeId",
                table: "WorkflowAssignments",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignments_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowAssignments",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowAssignments_Workflows_WorkflowId",
                table: "WorkflowAssignments",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdges_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowEdges",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdges_WorkflowNodes_WorkflowNodeId1",
                table: "WorkflowEdges",
                column: "WorkflowNodeId1",
                principalTable: "WorkflowNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowEdges_Workflows_WorkflowId",
                table: "WorkflowEdges",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowForms_Forms_FormId",
                table: "WorkflowForms",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowForms_Workflows_WorkflowId",
                table: "WorkflowForms",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstances_AspNetUsers_CreatedBy",
                table: "WorkflowInstances",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstances_Workflows_WorkflowId",
                table: "WorkflowInstances",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstanceSteps_WorkflowInstances_InstanceId",
                table: "WorkflowInstanceSteps",
                column: "InstanceId",
                principalTable: "WorkflowInstances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowInstanceSteps_WorkflowNodes_WorkflowNodeId",
                table: "WorkflowInstanceSteps",
                column: "WorkflowNodeId",
                principalTable: "WorkflowNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowNodes_Workflows_WorkflowId",
                table: "WorkflowNodes",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowProgresses_WorkflowStatuses_CurrentStatusId",
                table: "WorkflowProgresses",
                column: "CurrentStatusId",
                principalTable: "WorkflowStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowProgresses_Workflows_WorkflowId",
                table: "WorkflowProgresses",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRuleActions_WorkflowRules_RuleId",
                table: "WorkflowRuleActions",
                column: "RuleId",
                principalTable: "WorkflowRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRuleConditions_WorkflowRules_RuleId",
                table: "WorkflowRuleConditions",
                column: "RuleId",
                principalTable: "WorkflowRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowRules_Workflows_WorkflowId",
                table: "WorkflowRules",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_AspNetUsers_CreatedBy",
                table: "Workflows",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowStatuses_Workflows_WorkflowId",
                table: "WorkflowStatuses",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransitions_WorkflowStatuses_FromStatusId",
                table: "WorkflowTransitions",
                column: "FromStatusId",
                principalTable: "WorkflowStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransitions_WorkflowStatuses_ToStatusId",
                table: "WorkflowTransitions",
                column: "ToStatusId",
                principalTable: "WorkflowStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkflowTransitions_Workflows_WorkflowId",
                table: "WorkflowTransitions",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAssignments_WorkOrders_WorkOrderId",
                table: "WorkOrderAssignments",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAttachments_AspNetUsers_UploadedBy",
                table: "WorkOrderAttachments",
                column: "UploadedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderAttachments_WorkOrders_WorkOrderId",
                table: "WorkOrderAttachments",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEquipmentRequirements_Equipment_EquipmentId",
                table: "WorkOrderEquipmentRequirements",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderEquipmentRequirements_WorkOrders_WorkOrderId",
                table: "WorkOrderEquipmentRequirements",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AspNetUsers_AssignedTechnicianId",
                table: "WorkOrders",
                column: "AssignedTechnicianId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AspNetUsers_CreatedBy",
                table: "WorkOrders",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrders",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_WorkflowStatuses_WorkflowStatusId",
                table: "WorkOrders",
                column: "WorkflowStatusId",
                principalTable: "WorkflowStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Workflows_WorkflowId",
                table: "WorkOrders",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderSkillRequirements_Skills_SkillId",
                table: "WorkOrderSkillRequirements",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderSkillRequirements_WorkOrders_WorkOrderId",
                table: "WorkOrderSkillRequirements",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirements_Equipment_EquipmentId",
                table: "WorkOrderTypeEquipmentRequirements",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeEquipmentRequirements_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrderTypeEquipmentRequirements",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeSkillRequirements_Skills_SkillId",
                table: "WorkOrderTypeSkillRequirements",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderTypeSkillRequirements_WorkOrderTypes_WorkOrderTypeId",
                table: "WorkOrderTypeSkillRequirements",
                column: "WorkOrderTypeId",
                principalTable: "WorkOrderTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderZones_WorkOrders_WorkOrderId",
                table: "WorkOrderZones",
                column: "WorkOrderId",
                principalTable: "WorkOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrderZones_Zones_ZoneId",
                table: "WorkOrderZones",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
