function CourseListModel() {

   

    this.Load = function (callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Course/GetCourseList",
            data: "",
            dataType: "json",
            success: function (courseListData) {
                callback(courseListData);
            },
            error: function () {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.LoadSingle = function (id,callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Course/GetCourseById/" + id,
            data: "",
            dataType: "json",
            success: function (courseListData) {
                callback(courseListData);
            },
            error: function () {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.Update = function (scheduleData, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/Course/UpdateCourse",
            data: scheduleData,
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while updating schedule info');
            }
        });
    };

    this.Delete = function (course, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/Course/DeleteCourse",
            data: course,
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while deleteing course  info');
            }
        });
    };

    this.Insert = function (course, callback) {
        $.ajax({
            method: 'POST',
            url: "http://localhost:9393/Api/Course/InsertCourse",
            data: course,
            success: function (message) {
                callback(message);
            },
            error: function () {
                callback('Error while deleteing course  info');
            }
        });
    };
}
