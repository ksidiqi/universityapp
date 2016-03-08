function InstructorViewModel() {
    var self = this;
    var initialBind = false;
    var viewModel = null;

    var instructorModelObj = new InstructorModel();

    var CreateViewModel = function () {
        this.instructorList = ko.observableArray([]);
        this.instructorList2 = ko.observableArray([]);
        this.newInstructor =
        {
            FirstName: ko.observable(''),
            LastName: ko.observable(''),
            Title: ko.observable(''),          
        }

        this.AddNewCourseSchedule = AddNewCourseSchedule;     
    };
   

    var AddNewCourseSchedule = function (data) {
       
            var courseScheduleData = {
                FirstName: viewModel.newInstructor.FirstName(),
                LastName: viewModel.newInstructor.LastName(),
                Title: viewModel.newInstructor.Title()      
            };

            instructorModelObj.Insert(courseScheduleData, function (result) {
                if (result = 'ok') {
                    LoadInstructor("");
                    viewModel.newInstructor.FirstName("");
                    viewModel.newInstructor.LastName("");
                    viewModel.newInstructor.Title("");
                }
                else {
                    alert('Error occurs during Insert new Course Schedule!!');
                }
            });
        
    };

    var LoadInstructor = function () {
        var instructorList = new Array();

        instructorModelObj.Load(function (instructorListData) {
            for (var i = 0; i < instructorListData.length; i++) {
                instructorList[i] =
                    {
                        Id: instructorListData[i].Id,
                        FirstName: instructorListData[i].FirstName,
                        LastName: instructorListData[i].LastName,
                        Title: instructorListData[i].Title,

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
                        
                        deletePerson: function(data)
                        {
                            self.Delete(data);
                        }
                    };
            };

            if (viewModel != null) {
                viewModel.instructorList(instructorList);
            }
        });
    };

    this.refresh = function () {
        LoadCourse();
    };
    
    this.Load = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        LoadInstructor();


        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divManageCourseSchedule"));
        }
    };

    this.Update = function(data)
    {      
        var instructorList = new Array();
        var error = false;

        var instructorData = {
            Id: data.Id,
            FirstName: data.FirstName,
            LastName: data.LastName,
            Title: data.Title,
        };

        instructorModelObj.Load(function (instructorListData) 
        {
            for (var i = 0; i < instructorListData.length; i++) 
            {
                instructorList[i] =
                    {
                        Id: instructorListData[i].Id,
                        FirstName: instructorListData[i].FirstName,
                        LastName: instructorListData[i].LastName,
                        Title: instructorListData[i].Title
                    };
            };
           // alert(instructorListData[4].FirstName);
            for(var i = 0; i < instructorListData.length; i++)
            {
                
                if(instructorData.FirstName == instructorList[i].FirstName && instructorData.LastName == instructorList[i].LastName)
                {
                    //alert("Duplicate Instructor");
                    
                    error = true;
                }
                else {
                    error = false;
                }
            };

            if (error == true) {
                alert("Instructor Already Exists");
            }
            else {
                instructorModelObj.Update(instructorData, function (message) {
                    if (message == "ok") {
                        LoadInstructor();
                    }

                });
            };

        });

    };


    this.Delete = function (data)
    {
        var instructorData = {
            Id: data.Id,
            FirstName: data.FirstName,
            LastName: data.LastName,
            Title: data.Title,
        };

        instructorModelObj.Delete(instructorData, function (message) {
            if (message == "ok") {
                LoadInstructor();
            }

        });
    };

}
