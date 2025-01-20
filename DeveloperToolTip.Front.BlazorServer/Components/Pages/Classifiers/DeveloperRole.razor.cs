using DeveloperToolTip.Front.BlazorServer.Models;
using Radzen;

namespace DeveloperToolTip.Front.BlazorServer.Components.Pages.Classifiers
{
    public partial class DeveloperRole
    {
        private IEnumerable<RoleDto> Roles { get; set; } = Enumerable.Empty<RoleDto>();
        //private RoleDto SelectedRole = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadRoles();
        }

        private async Task LoadRoles()
        {
            Roles = await _IRoleService.GetAllRoles();
            StateHasChanged();
        }

        private async Task OpenCreateDialog()
        {
            var result = await _DialogService.OpenAsync<CreateRoleDialog>("Create Role");
            if (result != null)
            {
                var newRole = new CreateRoleDto { RoleName = result.ToString() };
                //var _newRole = MapToCreateRoleDto(SelectedRole);
                var success = await _IRoleService.CreateRole(newRole);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Role created successfully!");
                    await LoadRoles();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to create Role");
                }
            }
        }

        private async Task OpenEditDialog(RoleDto role)
        {
            var result = await _DialogService.OpenAsync<EditRoleDialog>("Edit Role",
            new Dictionary<string, object> { { "Role", role } });

            if (result != null)
            {
                role.RoleName = result.ToString();
                var success = await _IRoleService.UpdateRole(role);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Role updated successfully!");
                    await LoadRoles();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to update Role");
                }
            }
        }

        private async Task OpenDeleteDialog(RoleDto role)
        {
            var confirm = await _DialogService.Confirm("Are you sure you want to delete this Role?",
                $"Delete {role.RoleName}",
                new ConfirmOptions() { OkButtonText = "Yes", CancelButtonText = "No" });

            if (confirm == true)
            {
                var success = await _IRoleService.DeleteRole(role.Id);

                if (success)
                {
                    NotificationService.Notify(NotificationSeverity.Success, "Success", "Role deleted successfully!");
                    await LoadRoles();
                }
                else
                {
                    NotificationService.Notify(NotificationSeverity.Error, "Error", "Failed to delete Role");
                }
            }
        }

        // private CreateRoleDto MapToCreateRoleDto(RoleDto role)
        // {
        //     return new CreateRoleDto
        //         {
        //             RoleName = role.RoleName
        //         };
        // }

        private void OnRowSelect(RoleDto role)
        {
            NotificationService.Notify(NotificationSeverity.Info, "Selected", $"Selected Role: {role.RoleName}");
        }
    }
}
