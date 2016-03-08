function ManageCourseScheduleViewModel() {

    var allQuarters = ["Winter", "Spring", "Summer 1", "Summer 2", "Fall"];
    var allYears = ["2015", "2016", "2017"]; //temporary
    var allSessions = ["A00", "A01", "B00", "B01"];
    var self = this;
    var initialBind = false;
    var viewModel = null;
    var currentQuarter = {
        year: '',
        quarter: ''
    };
    var courseScheduleModelObj = new CourseScheduleModel();

    var CreateViewModel = function () {
        this.courseList = ko.observableArray([]);
        this.futureCourseScheduleList = ko.observableArray([]);
        this.previousCourseScheduleList = ko.observableArray([]);
        this.currentCourseScheduleList = ko.observableArray([]);
        this.newCourse = {
            CourseId: ko.observable(),
            Year: ko.observable(""),
            Quarter: ko.observable(""),
            Session: ko.observable(""),
            ScheduleDayId: ko.observable(),
            ScheduleTimeId: ko.observable(),
            InstructorId: ko.observable()
        };
        this.yearList = ko.observableArray(allYears);
        this.quarterList = ko.observableArray(allQuarters);
        this.sessionList = ko.observableArray(allSessions);
        this.scheduleTimeList = ko.observableArray([]);
        this.scheduleDayList = ko.observableArray([]);
        this.instructorList = ko.observableArray([]);
        this.AddNewCourseSchedule = AddNewCourseSchedule;
    };

    var AddNewCourseSchedule = function (data) {
        if (viewModel.newCourse.CourseId() != '' && viewModel.newCourse.Year() != '' &&
            viewModel.newCourse.Quarter() != '' && viewModel.newCourse.Session() &&
            viewModel.newCourse.ScheduleDayId() != '' && viewModel.newCourse.ScheduleTimeId() != '' &&
            viewModel.newCourse.InstructorId() != '') {

            var courseScheduleData = {
                Year: viewModel.newCourse.Year(),
                Quarter: viewModel.newCourse.Quarter(),
                Session: viewModel.newCourse.Session(),
                
                    Schedule_day_id: viewModel.newCourse.ScheduleDayId()
                ,
                
                    Schedule_time_id: viewModel.newCourse.ScheduleTimeId(),
                
                Instructor: {
                    Id: viewModel.newCourse.InstructorId()
                },
                Course: {
                    CourseId: viewModel.newCourse.CourseId
                }
            };

            courseScheduleModelObj.InsertCourseSchedule(courseScheduleData, function (result) {
                if (result = 'ok') {
                    LoadCourseSchedule("");
                    viewModel.newCourse.CourseId("");
                    viewModel.newCourse.Year("");
                    viewModel.newCourse.Quarter("");
                    viewModel.newCourse.Session("");
                    viewModel.newCourse.ScheduleDayId("");
                    viewModel.newCourse.ScheduleTimeId("");
                    viewModel.newCourse.InstructorId("");
                }
                else {
                    alert('Error occurs during Insert new Course Schedule!!');
                }
            });
        }
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

    var LoadScheduleTime = function () {
        var scheduleTimeList = new Array();

        courseScheduleModelObj.LoadScheduleTime(function (result) {
            for (var i = 0; i < result.length; i++)
            {
                scheduleTimeList[i] = {
                    ScheduleTimeId: result[i].Schedule_time_id,
                    Time: result[i].Schedule_time
                };
            };

            if (viewModel != null) {
                viewModel.scheduleTimeList(scheduleTimeList);
            }
        });
    };

    var LoadScheduleDay = function () {
        var scheduleDayList = new Array();

        courseScheduleModelObj.LoadScheduleDay(function (result) {
            for (var i = 0; i < result.length; i++) {
                scheduleDayList[i] =
                {
                    ScheduleDayId: result[i].Schedule_day_id,
                    Day: result[i].Schedule_day
                };
            };

            if (viewModel != null) {
                viewModel.scheduleDayList(scheduleDayList);
            }
        });
    };

    var LoadInstructor = function () {
        var instructorList = new Array();

        courseScheduleModelObj.LoadInstructor(function (result) {
            for (var i = 0; i < result.length; i++) {
                instructorList[i] = {
                    InstructorId: result[i].Id,
                    FirstName: result[i].FirstName,
                    LastName: result[i].LastName
                };
            };

            if (viewModel != null) {
                viewModel.instructorList(instructorList);
            }
        });
    };

    var GetInstructorName = function (id) {
        for (var i = 0; i < viewModel.instructorList().length; i++) {
            if (id == viewModel.instructorList()[i].InstructorId) {
                return viewModel.instructorList()[i].FirstName + ' ' + viewModel.instructorList()[i].LastName;
            }
        }
    };

    var GetDay = function (id) {
        for (var i = 0; i < viewModel.scheduleDayList().length; i++) {
            if (id == viewModel.scheduleDayList()[i].ScheduleDayId) {
                return viewModel.scheduleDayList()[i].Day;
            }
        }
    };

    var GetTime = function (id) {
        for (var i = 0; i < viewModel.scheduleTimeList().length; i++) {
            if (id == viewModel.scheduleTimeList()[i].ScheduleTimeId) {
                return viewModel.scheduleTimeList()[i].Time;
            }
        }
    };

    var LoadCourseSchedule = function () {
        var courseScheduleList = new Array();

        courseScheduleModelObj.LoadCourseSchedule(function (courseScheduleListData) {
            for (var i = 0; i < courseScheduleListData.length; i++) {
                courseScheduleList[i] = {
                    schedule_id: courseScheduleListData[i].ScheduleId,
                    year: courseScheduleListData[i].Year,
                    quarter: courseScheduleListData[i].Quarter,
                    session: courseScheduleListData[i].Session,
                    course_id: courseScheduleListData[i].Course.CourseId,
                    course_title: courseScheduleListData[i].Course.Title,
                    course_description: courseScheduleListData[i].Course.Description,
                    instructor_id: courseScheduleListData[i].Instructor.Id,
                    schedule_day_id: courseScheduleListData[i].Schedule_day_id,
                    schedule_time_id: courseScheduleListData[i].Schedule_time_id,
                    instructor_name: GetInstructorName(courseScheduleListData[i].Instructor.Id),
                    day: GetDay(courseScheduleListData[i].Schedule_day_id),
                    time: GetTime(courseScheduleListData[i].Schedule_time_id),

                    edit: ko.observable(false),
                    change: function (data) {
                        data.edit(true);
                    },
                    cancel: function (data) {
                        data.edit(false);
                    },

                    update: function(data)
                    {
                        self.Update(data);
                    }
                };
            };


            AddCourseScheduleToViewModel(courseScheduleList);
        });
    };

    var AddCourseScheduleToViewModel = function (courseScheduleList) {
        var currentCourseScheduleList = new Array();
        var previousCourseScheduleList = new Array();
        var futureCourseScheduleList = new Array();

        for (var i = 0; i < courseScheduleList.length; i++) {
            if (courseScheduleList[i].year < currentQuarter.year) {
                previousCourseScheduleList.push(courseScheduleList[i]);
            }
            else if (courseScheduleList[i].year > currentQuarter.year) {
                futureCourseScheduleList.push(courseScheduleList[i]);
            }
            else {
                var id = 0;
                var cur_id = 0;
                switch (courseScheduleList[i].quarter) {
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

        }
    };

    this.Load = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        LoadTimeInterval();
        LoadScheduleTime();
        LoadScheduleDay();
        LoadInstructor();
        LoadCourse();
        LoadCourseSchedule();

        if (!initialBind) {
            initialBind = true;
            ko.applyBindings({ viewModel: viewModel }, document.getElementById("divManageCourseSchedule"));
        }
    };

    this.Update = function (data) {
        var ScheduleData =
            {
                ScheduleId: data.schedule_id,
                Year: data.year,
                Quarter: data.quarter,
                Session: data.session,
                Schedule_day_id: data.schedule_day_id,
                Schedule_time_id: data.schedule_time_id,
                Instructor:
                {
                    Id: data.instructor_id
                },
                Course:
                {
                    CourseId: data.course_id
                }
            };

        courseScheduleModelObj.UpdateCourseSchedule(ScheduleData, function (message)
        {
            self.Load();

        });
    };
}
