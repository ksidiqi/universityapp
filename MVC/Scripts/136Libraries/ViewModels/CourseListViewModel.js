function CourseListViewModel() {
    var self = this;
    var initialBind = false;
    var viewModel = null;
    var Levels = ["0", "1", "2"];

    var courseModelObj = new CourseListModel();

    var CreateViewModel = function () {
        this.courseList = ko.observableArray([]);
        this.instructorList2 = ko.observableArray([]);
        this.newCourse =
        {
            Title: ko.observable(''),
            Description: ko.observable(''),
            Level: ko.observable(''),
        }
        this.levelList = ko.observableArray(Levels);


        this.AddNewCourse = AddNewCourse;
    };

    var AddNewCourse = function (data) {

        var courseData = {
            Title: viewModel.newCourse.Title(),
            Description: viewModel.newCourse.Description(),
            CourseLevel: viewModel.newCourse.Level()
        };

        courseModelObj.Insert(courseData, function (result) {
            if (result = 'ok') {
                LoadCourse("");
                viewModel.newCourse.Title("");
                viewModel.newCourse.Description("");
                viewModel.newCourse.Level("");
            }
            else {
                alert('Error occurs during Insert new Course Schedule!!');
            }
        });

    };

    var LoadCourse = function () {
        var courseList = new Array();

        courseModelObj.Load(function (courseListData) {
            for (var i = 0; i < courseListData.length; i++) {
                courseList[i] =
                    {
                        Id:courseListData[i].CourseId,
                        Title: courseListData[i].Title,
                        Description: courseListData[i].Description,
                        Level: courseListData[i].CourseLevel,

                        edit: ko.observable(false),

                        change: function (data) {
                            data.edit(true);
                        },
                        cancel: function (data) {
                            data.edit(false);
                            self.refresh(data);
                        },
                        accept: function (data) {
                            self.Update(data);
                        },

                        deleteCourse: function (data) {
                            self.Delete(data);
                        }
                    };
               
                    
            };

            if (viewModel != null) {
                viewModel.courseList(courseList);
            }
        });
    };

    this.refresh = function () {
        self.Load();
    };

    this.Load = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        LoadCourse();


        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divCourseListContent"));
        }
    };

    this.Update = function (data) {
        var courseList = new Array();
        var error = false;

        var courseData = {
            CourseId: data.Id,
            Title: data.Title,
            Description: data.Description,
            CourseLevel: data.Level,
        };

    
        courseModelObj.Update(courseData, function (message) {
            if (message == "ok") 
            {
                LoadCourse();
            }

        });
          
    };

    this.Delete = function (data) {
        var courseData = {
            CourseId: data.Id,
        };

       courseModelObj.Delete(courseData, function (message) {
           if (message == "ok") {
                LoadCourse();
            }

        });
    };


}
