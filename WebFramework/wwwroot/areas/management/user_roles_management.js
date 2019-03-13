
// controller name RolesController

class RoleManagerment {
    constructor() {
    }

    getAddToRoleUrl() {
        return '/Management/UserRoles/AddRole';
    }

    getRemoveToRoleUrl() {
        return '/Management/UserRoles/RemoveRole';
    }

    showSuccessMessage(successMessage) {
        alert(successMessage);
    }

    showErrorMessage(errorMessage) {
        alert(errorMessage);
    }

    onRoleChecked(checkedCallback) {
        $('.role-checked').change(checkedCallback);
    }

    addUserToRole(userId, roleName, successCallback, errorCallback) {
        $.post(this.getAddToRoleUrl() + String.Format('?userId={0}&roleName={1}', userId, roleName))
            .done(successCallback).fail(errorCallback);
    }

    removeUserFromRole(userId, roleName, successCallback, errorCallback) {
        $.delete(this.getRemoveToRoleUrl() + String.Format('?userId={0}&roleName={1}', userId, roleName)).done(successCallback).fail(errorCallback);
    }

}

var roleManagement = new RoleManagerment();

roleManagement.onRoleChecked(function () {
    var roleName = this.name;
    var userId = $(this).attr('data-user-id');
    var isChecked = this.checked;
    if (isChecked) {
        roleManagement.addUserToRole(userId, roleName, function (response) {
            if (response.result === "success") {
                //add success
                roleManagement.showSuccessMessage(String.Format("add role {0} to user {1} success", roleName, userId));
            }
            else {
                //add failed
                roleManagement.showErrorMessage("failed");
            }
        }, function () {
        });
    }
    else {
        roleManagement.removeUserFromRole(userId, roleName, function (response) {
            if (response.result === "success") {
                //add success
                roleManagement.showSuccessMessage(String.Format("remove role {0} from user {1} success", roleName, userId));
            }
            else {
                //add failed
                roleManagement.showErrorMessage("failed");
            }
        }, function () {
        });
    }
});