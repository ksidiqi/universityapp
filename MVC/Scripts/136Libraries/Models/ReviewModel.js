function ReviewListModel() {

    this.GetAll = function (callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetReviewList",
            data: "",
            dataType: "json",
            success: function (reviewListData) {
                callback(reviewListData);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };

    this.GetStudentReview = function (id, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetStudentReview?id=" + id,
            data: "",
            dataType: "json",
            success: function (reviewListData) {
                callback(reviewListData);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };


    this.GetCourseReview = function (id, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetCourseReview?id=" + id,
            data: "",
            dataType: "json",
            success: function (reviewListData) {
                callback(reviewListData);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };

    this.GetAverageReview = function (id, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetAverageCourseReview?id=" + id,
            data: "",
            dataType: "json",
            success: function (review) {
                callback(review);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };

    this.GetAverageProfReview = function (id, tmp, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetAverageProfCourseReview?id=" + id,
            data: "",
            dataType: "json",
            success: function (review) {
                callback(tmp, review);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };


    this.GetAllProfReview = function (id, callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Review/GetProfessorReview?id="+id,
            method: "GET",
            data: "",
            dataType: "json",
            success: function (reviewListData) {
                callback(reviewListData);
            },
            error: function () {
                alert('Error while loading Review list.  Is your service layer running?');
            }
        });
    };

  

    this.edit = function (data, callback) {

        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Review/UpdateReview" ,
            data: data,
            dataType: "json",
            success: function (result) {
                callback(result);

            },
            error: function () {
                alert('Error while deleteing student.  Is your service layer running?');
            }
        });
    };

    this.Create = function (data, callback) {

        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Review/InsertReview",
            data: data,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while inserting review.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (data, callback) {

        $.ajax({
            method: "POST",
            url: "http://localhost:9393/Api/Review/DeleteReview",
            data: data,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while inserting review.  Is your service layer running?');
            }
        });
    };
}
