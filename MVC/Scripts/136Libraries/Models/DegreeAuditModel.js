function DegreeAuditModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.GetDegreeAudit = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/Api/DegreeAudit/GetStudentDegreeAudit/" + id,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading degree audit list.  Is your service layer running?');
            }
        });
    };

    this.AddDegreeAudit = function (degAudit, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/DegreeAudit/InsertDegreeAudit",
            data: degAudit,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading degree audit list.  Is your service layer running?');
            }
        });
    };

    this.DeleteAudit = function (DegreeAudit, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/DegreeAudit/DeleteDegreeAudit",
            data: DegreeAudit,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading degree audit list.  Is your service layer running?');
            }
        });
    };

    this.Update = function (DegreeAudit, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/api/DegreeAudit/UpdateDegreeAudit",
            data: DegreeAudit,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading degree audit list.  Is your service layer running?');
            }
        });
    };



}
