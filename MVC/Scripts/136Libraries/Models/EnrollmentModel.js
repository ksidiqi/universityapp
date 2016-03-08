function EnrollmentModel(asyncIndicator) {
        if (asyncIndicator == undefined) {
            asyncIndicator = true;
        }

        this.DeleteEnrollment = function (id, sid, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:9393/Api/Student/DropEnrolledSchedule?student_id="+ id + "&schedule_id="+ sid,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while deleting enrollment.  Is your service layer running?');
                }
            });
        };

        this.CreateEnrollment = function (enroll, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "POST",
                url: "http://localhost:9393/Api/Student/EnrollSchedule",
                data: enroll,
                dataType: "json",
                success: function (result) {
                    //alert(result);
                    callback(result);
                },
                error: function () {
                    alert('Error while adding enrollment.  Is your service layer running?');
                }
            });
        };

        this.GetAllEnroll = function (id, callback) {
            $.ajax({
                async: asyncIndicator,
                method: "GET",
                url: "http://localhost:9393/Api/Student/GetEnrollment/" + id,
                data: "",
                dataType: "json",
                success: function (result) {
                    callback(result);
                },
                error: function () {
                    alert('Error while loading enrollment list.  Is your service layer running?');
                }
            });
        };
    }
  