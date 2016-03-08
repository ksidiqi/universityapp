function CourseScheduleModel() {

    this.LoadCourseSchedule = function (callback) {
        $.ajax({
            url: "http://localhost:9393/api/Schedulle/GetScheduleList",
            data: "",
            dataType: "json",
            success: function (courseScheduleListData) {
                callback(courseScheduleListData);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.LoadScheduleTime = function (callback) {
        $.ajax({
            url: "http://localhost:9393/api/Schedulle/GetScheduleTime",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.LoadScheduleDay = function (callback) {
        $.ajax({
            url: "http://localhost:9393/api/Schedulle/GetScheduleByDay",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.LoadInstructor = function (callback) {
        $.ajax({
            url: "http://localhost:9393/api/Instructor/GetInstructorList",
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.InsertCourseSchedule = function (schedule, callback) {
        $.ajax({
            url: "http://localhost:9393/api/Schedulle/InsertSchedule",
            data: schedule,
            dataType: "json",
            method: "POST",
            success: function (result) {
                callback(result);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.UpdateCourseSchedule = function (schedule, callback) {
        $.ajax({
            url: "http://localhost:9393/api/Schedulle/UpdateSchedule",
            data: schedule,
            dataType: "json",
            method: "POST",
            success: function (result) {
                callback(result);
            },
            error: function (x, e) {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };
}
