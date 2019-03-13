
// controller name RolesController

class RoleManagerment {
    constructor() {
    }

    getAddRoleForm() {
        return $('#add-role-form');
    }

    getUpdateRoleForm() {
        return $('#update-role-form');
    }


    getAddRoleVal() {
        return $('#add-role-form input[type="text"]').val();
    }

    getUpdateRoleVal() {
        return this.getUpdateInput().val();
    }

    getAddRoleUrl() {
        return "/Management/Roles/Create";
    }

    getUpdateRoleUrl() {
        return "/Management/Roles/Modifile";
    }

    getDeleteRoleUrl() {
        return "/Management/Roles/Delete";
    }

    getUpdateInput() {
        return $('.update-popup-input');
    }

    getRolesElement() {
        return $('#roles');
    }

    getDeleteElements() {
        return $('.delete-role');
    }


    getRoleElementByRoleId(roleId) {
        return this.getRolesElement().find(String.Format('.role-name[data-id="{0}"]', roleId));
    }

    postRole(role, successCallback, failedCallback) {
        $.post(String.Format("{0}?{1}={2}", this.getAddRoleUrl(), "role", role)).done(successCallback).fail(failedCallback);
    }

    putRole(roleId, newRoleName, successCallback, failedCallback) {
        $.put(String.Format("{0}?{1}={2}&{3}={4}", this.getUpdateRoleUrl(),
            "roleId",
            roleId,
            "newRoleName",
            newRoleName))
            .done(successCallback)
            .fail(failedCallback);
    }

    deleteRole(roleId, successCallback, failedCallback) {
        $.delete(String.Format("{0}?{1}={2}", this.getDeleteRoleUrl(),
            "roleId",
            roleId))
            .done(successCallback)
            .fail(failedCallback);
    }

    deleteElementClick(clickCallback) {
        $(document).delegate('.delete-role', 'click', clickCallback);
    }

    clearInput() {
        this.getAddRoleForm().find('input[type="text"]').val('');
    }

    submitAddForm(addCallback) {
        this.getAddRoleForm().submit(addCallback);
    }

    submitUpdateForm(updateCallback) {
        this.getUpdateRoleForm().submit(updateCallback);
    }

    onDoubleClickRole(dblclickCallback) {
        $(document).delegate('.role-name', 'dblclick', dblclickCallback);
    }

    showSuccess() {
        alert("success");
    }

    setUpdateVal(updateVal) {
        this.getUpdateInput().val(updateVal);
    }

    clearUpdateInputVal() {
        this.getUpdateInput().val('');
    }

    setAttrOldRoleUpdateInput(oldRole) {
        this.getUpdateInput().attr('data-old-value', oldRole);
    }
    getAttrOldRoleUpdateInput() {
        return this.getUpdateInput().attr('data-old-value');
    }
    removeAttrOldRoleUpdateInput() {
        this.getUpdateInput().prop('data-old-value', '');
    }

    showWaiting() {

    }

    hideWaiting() {

    }

    showFail(fail) {
        alert(fail);
    }

    showUpdatePopup() {
        $('#update-popup').show();
    }

    hideUpdatePopup() {
        $('#update-popup').hide();
    }

    calcArea() {
        return this.height * this.width;
    }

    appendNewRoleName(role) {
        $('#roles').append(String.Format('<p class="role-name">{0}</p>', role));
    }
}

var roleManagement = new RoleManagerment();

roleManagement.onDoubleClickRole(function (e) {
    roleManagement.showUpdatePopup();
    roleManagement.setAttrOldRoleUpdateInput($(this).attr('data-id'));
    roleManagement.setUpdateVal($(this).text());
});

roleManagement.submitAddForm(function (e) {
    e.preventDefault();
    var val = roleManagement.getAddRoleVal();
    roleManagement.showWaiting();
    roleManagement.postRole(val, function (response) {
        roleManagement.hideWaiting();
        if (response.result == "success") {
            roleManagement.showSuccess();
            roleManagement.clearInput();
            roleManagement.appendNewRoleName(response.data);
        }
        else {
            roleManagement.showFail(response.data);
        }
    }, function () {
        roleManagement.hideWaiting();
    });
});

roleManagement.deleteElementClick(function (e) {
    var roleId = $(this).attr('data-id');
    roleManagement.deleteRole(roleId, function () {
        roleManagement.getRoleElementByRoleId(roleId).parent().remove();
    }, function () {
    });
});

roleManagement.submitUpdateForm(function (e) {
    e.preventDefault();
    var roleId = roleManagement.getAttrOldRoleUpdateInput();
    var newRoleName = roleManagement.getUpdateRoleVal();
    roleManagement.putRole(roleId, newRoleName, function () {
        roleManagement.hideUpdatePopup();
        roleManagement.removeAttrOldRoleUpdateInput();
        roleManagement.clearUpdateInputVal();
        roleManagement.getRoleElementByRoleId(roleId).text(newRoleName);
    })
})