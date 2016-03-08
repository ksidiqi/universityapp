
    function EnrollmentViewModel() {
        var self = this;
        var initialBind = false;
        var viewModel = null;
        var allQuarters = ["Winter", "Spring", "Summer 1", "Summer 2", "Fall"];
        var allYears = ["2015", "2016", "2017"]; //temporary
        var allSessions = ["A00", "A01", "B00", "B01"];
        var currentQuarter = {
            year: '',
            quarter: ''
        };

        

        var enrollmentModelObj = new EnrollmentModel();

        var CreateViewModel = function (id) {
            this.enrollmentList = ko.observableArray([]);
            this.instructorList2 = ko.observableArray([]);
            this.futureCourseScheduleList = ko.observableArray([]);
            this.previousCourseScheduleList = ko.observableArray([]);
            this.currentCourseScheduleList = ko.observableArray([]);

            this.yearList = ko.observableArray(allYears);
            this.quarterList = ko.observableArray(allQuarters);

            this.newSchedule =
            {
                Id: ko.observable(''),
                StudentId: ko.observable(id),
            }
            this.AddNewEnrollment = AddNewEnrollment;

        };


        var LoadTimeInterval = function () {
            var date = new Date();
            currentQuarter.year = date.getFullYear();
            allYears = [currentQuarter.year, currentQuarter.year + 1, currentQuarter.year + 2];

            //current quarter
            var currentMonth = date.getMonth() + 1;
            if (currentMonth == 12 || currentMonth <= 2) {
                currentQuarter.quarter = "Winter";
            }
            else if (currentMonth >= 3 && currentMonth <= 5) {
                currentQuarter.quarter = "Spring";
            }
            else if (currentMonth == 6 || currentMonth == 7) {
                currentQuarter.quarter = "Summer 1";
            }
            else if (currentMonth == 8 || currentMonth == 9) {
                currentQuarter.quarter = "Summer 2";
            }
            else if (currentMonth >= 10 && currentMonth <= 11) {
                currentQuarter.quarter = "Fall";
            }
        };


        var AddNewEnrollment = function (data) {
            var courseScheduleData =
            {               
                StudentId: viewModel.newSchedule.StudentId(),


                Schedule:
                    {
                        ScheduleId: viewModel.newSchedule.Id()
                    }
               
            };

            scheduleModelObj = new ScheduleModel();
            scheduleModelObj.Load(viewModel.newSchedule.Id(), function (data) {
                if (data[0].Year < currentQuarter.year)
                {
                    alert("Course ALready Ended");
                }
                else if (data[0].Year > currentQuarter.year)
                {
                    enrollmentModelObj.CreateEnrollment(courseScheduleData, function (result) {
                        if (result = "ok") {

                            self.Load(viewModel.newSchedule.StudentId());
                            //LoadEnrollment(viewModel.newSchedule.StudentId());
                            viewModel.newSchedule.Id("");
                        }
                        else if (result.match("error")) {
                            alert('Error occurs during Insert new Course Schedule!!');
                        }
                    });
                }
                else {
                    var id = 0;
                    var cur_id = 0;
                    switch (data[0].Quarter) {
                        case 'Winter':
                            id = 1;
                            break;
                        case 'Spring':
                            id = 2;
                            break;
                        case 'Summer 1':
                            id = 3;
                            break;
                        case 'Summer 2':
                            id = 4;
                            break;
                        case 'Fall':
                            id = 5;
                            break;
                    }

                    switch (currentQuarter.quarter) {
                        case 'Winter':
                            cur_id = 1;
                            break;
                        case 'Spring':
                            cur_id = 2;
                            break;
                        case 'Summer 1':
                            cur_id = 3;
                            break;
                        case 'Summer 2':
                            cur_id = 4;
                            break;
                        case 'Fall':
                            cur_id = 5;
                            break;
                    }

                    if (cur_id > id) {
                       alert("course alredy ended")
                    }
                    else if (cur_id < id) {
                        enrollmentModelObj.CreateEnrollment(courseScheduleData, function (result) {
                            if (result = "ok") {

                                self.Load(viewModel.newSchedule.StudentId());
                                //LoadEnrollment(viewModel.newSchedule.StudentId());
                                viewModel.newSchedule.Id("");
                            }
                            else if (result.match("error")) {
                                alert('Error occurs during Insert new Course Schedule!!');
                            }
                        });
                    }
                    else {
                        enrollmentModelObj.CreateEnrollment(courseScheduleData, function (result) {
                            if (result = "ok") {

                                self.Load(viewModel.newSchedule.StudentId());
                                //LoadEnrollment(viewModel.newSchedule.StudentId());
                                viewModel.newSchedule.Id("");
                            }
                            else if (result.match("error")) {
                                alert('Error occurs during Insert new Course Schedule!!');
                            }
                        });
                    }

                }
            });


           

        };

        var LoadEnrollment = function (id)
        {
            var enrollmentList = new Array();
            //alert("calling loadenrollment");

            enrollmentModelObj.GetAllEnroll(id, function (enrollData)
            {
                
                //if (enrollData != null)
                //{
                    for (var i = 0; i < enrollData.length; i++)
                    {
                        if (enrollData[i].Schedule.Course.CourseLevel == "0") {
                            var levelCourse = "Lower Division";
                        }
                        else if (enrollData[i].Schedule.Course.CourseLevel == "1") {
                            var levelCourse = "Upper Division";
                        }
                        else {
                            var levelCourse = "Graduate Course"
                        };
                        enrollmentList[i] =
                            {
                                SchedId: enrollData[i].Schedule.ScheduleId,
                                Id: enrollData[i].StudentId,
                                Title: enrollData[i].Schedule.Course.Title,
                                Description: enrollData[i].Schedule.Course.Description,
                                Level: levelCourse,
                                Instructor_fn: enrollData[i].Schedule.Instructor.FirstName,
                                Instructor_ln: enrollData[i].Schedule.Instructor.LastName,
                                Quarter: enrollData[i].Schedule.Quarter,
                                Year: enrollData[i].Schedule.Year,

                                ScheduleDay: enrollData[i].Schedule.Schedule_day,

                                ScheduleTime: enrollData[i].Schedule.Schedule_time,


                                deleteEnrollment: function (data) {
                                    self.Delete(data);
                                }


                            };
                    };
                    AddEnrollmentToViewModel(enrollmentList);


                //     if (viewModel != null) {
                //        viewModel.enrollmentList(enrollmentList);
                //    }
                //}

                //if(enrollData == null)
                //{
                //    var empty = new Array();
                //    viewModel.enrollmentList(empty);
                //}
            });
        };


        var AddEnrollmentToViewModel = function (courseScheduleList) {
            var currentCourseScheduleList = new Array();
            var previousCourseScheduleList = new Array();
            var futureCourseScheduleList = new Array();
            
            
            for (var i = 0; i < courseScheduleList.length; i++) {
                if (courseScheduleList[i].Year < currentQuarter.year) {
                    previousCourseScheduleList.push(courseScheduleList[i]);
                }
                else if (courseScheduleList[i].Year > currentQuarter.year) {
                    futureCourseScheduleList.push(courseScheduleList[i]);
                }
                else
                {
                    var id = 0;
                    var cur_id = 0;
                    switch (courseScheduleList[i].Quarter) {
                        case 'Winter':
                            id = 1;
                            break;
                        case 'Spring':
                            id = 2;
                            break;
                        case 'Summer 1':
                            id = 3;
                            break;
                        case 'Summer 2':
                            id = 4;
                            break;
                        case 'Fall':
                            id = 5;
                            break;
                    }

                    switch (currentQuarter.quarter)
                    {
                        case 'Winter':
                            cur_id = 1;
                            break;
                        case 'Spring':
                            cur_id = 2;
                            break;
                        case 'Summer 1':
                            cur_id = 3;
                            break;
                        case 'Summer 2':
                            cur_id = 4;
                            break;
                        case 'Fall':
                            cur_id = 5;
                            break;
                    }

                    if (cur_id > id) {
                        previousCourseScheduleList.push(courseScheduleList[i]);
                    }
                    else if (cur_id < id) {
                        futureCourseScheduleList.push(courseScheduleList[i]);
                    }
                    else {
                        currentCourseScheduleList.push(courseScheduleList[i]);
                    }

                }
            };

            if (viewModel != null) {
                viewModel.currentCourseScheduleList(currentCourseScheduleList);
                viewModel.futureCourseScheduleList(futureCourseScheduleList);
                viewModel.previousCourseScheduleList(previousCourseScheduleList);
                viewModel.enrollmentList(courseScheduleList);
            }
        };



        this.Delete = function(data)
        {
            var SchedData = {
                ScheduleId: data.SchedId,
                FirstName: data.FirstName,
                LastName: data.LastName,
                Title: data.Title,
            };

            enrollmentModelObj.DeleteEnrollment(data.Id, data.SchedId, function (message)
            {
               // alert(data.SchedId);
                self.Load(data.Id);
            });
        }


        this.Load = function (id) {
            if (viewModel == null) {
                viewModel = new CreateViewModel(id);
            }
            LoadTimeInterval();

            LoadEnrollment(id);


            if (!initialBind) {
                initialBind = true;
                ko.applyBindings({ viewModel: viewModel }, document.getElementById("divEnrollment"));
            }
        };

    }
