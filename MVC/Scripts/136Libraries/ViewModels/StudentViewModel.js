function StudentViewModel() {

    var self = this;
    var initialBind = false;
    var viewModel = null;

    var studentModelObj = new StudentModel();


    var CreateViewModel = function () {
        this.studentList = ko.observableArray([]);
        this.studentInfos = ko.observableArray([]);
        this.newStudent =
        {
            Id: ko.observable(''),
            FirstName: ko.observable(''),
            LastName: ko.observable(''),
            SSN: ko.observable(''),
            Email: ko.observable(''),
            Password: ko.observable(''),
            ShoeSize: ko.observable(''),
            Weight: ko.observable(''),
        }
     
     
       this.studentInfo = 
       {
            Id: ko.observable('A12135'),
            FirstName: ko.observable('asdasd'),
            LastName: ko.observable('asdasd'),
            SSN: ko.observable('1111111111'),
            Email: ko.observable('asdsa'),
            Password: ko.observable('asdasd'),
            ShoeSize: ko.observable('10'),
            Weight: ko.observable('10'),
            edit: ko.observable(false),

            change: function (data) {
                data.edit(true);
            },
            cancel: function (data) {
                data.edit(false);
                self.refresh(data);
            },

            accept: function (data) {
                self.UpdateInfo(data);
            }
        }
     
        this.AddNewStudent = AddNewStudent;
    };

    var AddNewStudent = function (data)
    {
        var errors = false;
        var studentData =
            {
            StudentId: viewModel.newStudent.Id(),
            FirstName: viewModel.newStudent.FirstName(),
            LastName: viewModel.newStudent.LastName(),
            SSN: viewModel.newStudent.SSN(),
            Email: viewModel.newStudent.Email(),
            Password: viewModel.newStudent.Password(),
            ShoeSize: viewModel.newStudent.ShoeSize(),
            Weight: viewModel.newStudent.Weight()
        };
        
        if (viewModel.newStudent.Id().length < 5)
        {
            alert("Student Id Length must equal to > 5");
            errors = true;
        }
        else if(viewModel.newStudent.SSN().length != 9)
        {
            alert("SSN must be 9 digits");
            errors = true;
        }

        var dupe = false;
        studentModelObj.GetAll(function (studentListData)
        {
            for (var i = 0; i < studentListData.length; i++)
            {
                if (studentListData[i].SSN == viewModel.newStudent.SSN()) {
                    errors = true;
                    dupe = true;
                }  
            };
            if (dupe == false && errors == false) {
                studentModelObj.Create(studentData, function (result) {
                    if (result = 'ok') {
                        LoadStudent("");
                        viewModel.newStudent.Id(),
                        viewModel.newInstructor.FirstName();
                        viewModel.newInstructor.LastName();
                        viewModel.newInstructor.SSN();
                        viewModel.newInstructor.Email();
                        viewModel.newInstructor.Password();
                        viewModel.newInstructor.ShoeSize();
                        viewModel.newInstructor.Weight();
                    }
                    else {
                        alert('Error occurs during Insert new Course Schedule!!');
                    }
                });
            } else {
                alert("SSN Already EXists");
            };

   
        });

     

    };

    var LoadStudent = function ()
    {
        var studentList = new Array();

        studentModelObj.GetAll(function (studentListData)
        {
            for (var i = 0; i < studentListData.length; i++) {
                studentList[i] =
                    {
                        Id: studentListData[i].StudentId,
                        FirstName: studentListData[i].FirstName,
                        LastName: studentListData[i].LastName,
                        Email: studentListData[i].Email,
                        SSN: studentListData[i].SSN,
                        Weight: studentListData[i].Weight,
                        Password: studentListData[i].Password,
                        ShoeSize: studentListData[i].ShoeSize,


                        edit: ko.observable(false),

                        change: function (data) {
                            data.edit(true);
                        },
                        cancel: function (data) {
                            data.edit(false);
                            self.refresh(data);
                        },
                        //add here
                       
                        accept: function (data) {
                            self.Update(data);
                        },

                        deletePerson: function (data) {
                            self.Delete(data);
                        }
                    };
            };

            if (viewModel != null) {
                viewModel.studentList(studentList);
            }
        });
    };

    this.refresh = function () {
        LoadStudent();
    };

    this.Load = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        LoadStudent();
        

        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divStudentList"));
        }
    };

    this.Delete = function (data) {
        var studentData = {
            StudentId: data.Id,
            FirstName: data.FirstName,
            LastName: data.LastName,
            Email: data.Email,
        };

        studentModelObj.Delete(studentData, function (message) {
            if (message == "ok") {
                LoadStudent();
            }

        });
    };

    this.Update = function (data) {
        var studentList = new Array();
        var error = false;

        var studentData = {
            StudentId: data.Id,
            FirstName: data.FirstName,
            LastName: data.LastName,
            Email: data.Email,
            SSN: data.SSN,
            ShoeSize: data.ShoeSize,
            Password: data.Password,
            Weight: data.Weight
        };

   
        studentModelObj.UpdateStudent(studentData, function (message)
        {
            if (message == "ok") 
            {
                LoadStudent();
            }

       });
         
    };

    this.GetAll = function() {

        studentModelObj.GetAll(function (studentList)
        {
            studentListViewModel.removeAll();

            for (var i = 0; i < studentList.length; i++) {
                studentListViewModel.push({
                    id: studentList[i].StudentId,
                    first: studentList[i].FirstName,
                    last: studentList[i].LastName,
                    email: studentList[i].Email
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: studentListViewModel }, document.getElementById("divStudentListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };



    //for each student
    this.Load2 = function (id) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        LoadStudentDetail(id);


        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divStudentContent"));
        }
    };

    var LoadStudentDetail = function (id) {

        var studentList = new Array();

        studentModelObj.GetDetail(id, function (studentListData) {


            studentList[0] =
                {
                    Id: id,
                    FirstName: studentListData.FirstName,
                    LastName: studentListData.LastName,
                    Email: studentListData.Email,
                    SSN: studentListData.SSN,
                    Weight: studentListData.Weight,
                    Password: studentListData.Password,
                    ShoeSize: studentListData.ShoeSize,


                    edit: ko.observable(false),

                    change: function (data) {
                        data.edit(true);
                    },
                    cancel: function (data) {
                        data.edit(false);
                        self.refresh(data);
                    },
                    //add here
                    accept: function (data)
                    {
                        self.UpdateInfo(data);
                    }
                };


            if (viewModel != null) {
                viewModel.studentInfos(studentList);
            }

        });
    }


    this.UpdateInfo = function (data) {
        var studentList = new Array();
        var error = false;

        var studentData = {
            StudentId: data.Id,
            FirstName: data.FirstName,
            LastName: data.LastName,
            Email: data.Email,
            SSN: data.SSN,
            ShoeSize: data.ShoeSize,
            Password: data.Password,
            Weight: data.Weight
        };


        studentModelObj.UpdateStudent(studentData, function (message) {
            if (message == "ok") {
                LoadStudentDetail(data.Id);
            }

        });

    };
}
