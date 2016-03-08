
function DegreeAuditViewModel()
{
    var self = this;
    var initialBind = false;
    var viewModel = null;

    var degreeAuditModelObj = new DegreeAuditModel();

    var CreateViewModel = function (id) {
        this.courseList = ko.observableArray([]);
        this.degreeAuditList = ko.observableArray([]);
        this.instructorList2 = ko.observableArray([]);
        this.newAudit =
        {
            Title: ko.observable(''),
            Grade: ko.observable(''),
            StudentId : ko.observable(id),
        }
        this.AddNewAudit = AddNewAudit;
    };

    var AddNewAudit = function (data) {
        var degreeAuditData =
            {
                StudentId: viewModel.newAudit.StudentId(),
                CourseId: viewModel.newAudit.Title(),
                CourseGrade: viewModel.newAudit.Grade(),
            };

        degreeAuditModelObj.AddDegreeAudit(degreeAuditData, function (result) {
            if (result = "ok")
            {

                self.Load(viewModel.newAudit.StudentId());
                //LoadEnrollment(viewModel.newSchedule.StudentId());
                viewModel.newAudit.Title("");
                viewModel.newAudit.Grade("");

            }
            else if (result.match("error")) {
                alert('Error occurs during Insert new Course Schedule!!');
            }
        });
    };

    var LoadCourse = function () {
        var courseList = new Array();
        var courseListModelObj = new CourseListModel();

        courseListModelObj.Load(function (courseListData) {
            for (var i = 0; i < courseListData.length; i++) {
                courseList[i] = { course_id: courseListData[i].CourseId, title: courseListData[i].Title, description: courseListData[i].Description };
            };

            if (viewModel != null) {
                viewModel.courseList(courseList);
            }
        });
    };



    var LoadDegreeAudit = function (id) {
        var degreeAuditList = new Array();
        //alert("calling loadenrollment");

        degreeAuditModelObj.GetDegreeAudit(id, function (degreeAuditData) {

            if (degreeAuditData != null) {
                for (var i = 0; i < degreeAuditData.length; i++) {

                    degreeAuditList[i] =
                            {
                                StudentId: degreeAuditData[i].StudentId,
                                CourseId: degreeAuditData[i].CourseId,
                                Grade: degreeAuditData[i].CourseGrade,
                                Title: degreeAuditData[i].Course.Title,
                                Description: degreeAuditData[i].Course.Description,
                                Level: degreeAuditData[i].Course.CourseLevel,

                                edit: ko.observable(false),

                                change: function (data) {
                                    data.edit(true);
                                },
                                cancel: function (data) {
                                    data.edit(false);
                                    self.refresh(data);
                                },
                                accept: function (data)
                                {
                                    self.Update(data);
                                },
                        

                                deleteHistory: function (data) {
                                    self.Delete(data);
                                }


                            };
                };
                if (viewModel != null) {
                    viewModel.degreeAuditList(degreeAuditList);
                }
            }

            if (degreeAuditData == null) {
                var empty = new Array();
                viewModel.degreeAuditList(empty);
            }
        });
    };


    this.Load = function (id) {
        if (viewModel == null) {
            viewModel = new CreateViewModel(id);
        }
        LoadCourse();
        LoadDegreeAudit(id);


        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divDegreeAuditList"));
        }
    };

    this.Delete = function (data) {
        
        var Del = {
            StudentId: data.StudentId,
            CourseId: data.CourseId,          
        };

        degreeAuditModelObj.DeleteAudit(Del, function (message) {
            // alert(data.SchedId);
            self.Load(data.StudentId);
        });


    };


    this.Update = function (data) {
        var degreeAuditList = new Array();
        var error = false;

        var degData = {
            StudentId: data.StudentId,
            CourseId: data.CourseId,
            CourseGrade: data.Grade,
        };

    
        degreeAuditModelObj.Update(degData, function (message)
        {
                    if (message == "ok") {
                        self.Load(data.StudentId);
                    }

        });
           
    };
}
