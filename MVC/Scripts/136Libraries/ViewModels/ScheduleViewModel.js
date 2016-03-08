    function ScheduleViewModel() {
        var self = this;
        var ScheduleModelObj = new ScheduleModel();
        var that = this;
        var initialBind = true;
        var scheduleListViewModel = ko.observableArray();

        this.Initialize = function () {
            var viewModel = {
                scheduleid: ko.observable(""),
                courseid: ko.observable(""),
                year: ko.observable(""),
                quarter: ko.observable(""),
                session: ko.observable(""),
                scheduledayid: ko.observable(""),
                scheduletimeid: ko.observable(""),
                instructorid: ko.observable(""),
                add: function (data) {
                    that.CreateSchedule(data);
                }
            };
            if (!initialBind) {
                ko.applyBindings(viewModel, document.getElementById("divSchedule"));

                initialBind = true;
            }
            ko.applyBindings(viewModel, document.getElementById("divSchedule"));
        };

        this.CreateSchedule = function (data) {
            var model = {
                ScheduleId: data.scheduleid(),
                year: data.year(),
                quarter: data.quarter(),
                session: data.session(),
                schedule_day_id: data.scheduledayid(),              
                schedule_time_id: data.scheduletimeid(),
                Instructor:
                    {
                        Id: data.instructorid()
                    },
                Course:
                {
                    CourseId: data.courseid()
                }
            }

            ScheduleModelObj.CreateSchedule(model, function (result) {
                if (result == "ok") {
                    alert("Create schedule successful");
                } else {
                    alert("add error");
                }
            });

        };

        this.UpdateSchedule = function (viewModel) {
            var ScheduleData =
            {
                ScheduleId: viewModel.scheduleid,
                Year: viewModel.year,
                Quarter: viewModel.quarter,
                Session: viewModel.session,
                Schedule_day_id: viewModel.scheduledayid,
                Schedule_time_id:viewModel.scheduletimeid,
                Instructor:
                {
                    Id: viewModel.instructorid
                },
                Course:
                {
                    CourseId: viewModel.courseid
                }
            };

            ScheduleModelObj.Update(ScheduleData, function (message) {
                $('#divMessage').html(message);
                self.Initialize();

            });

           

        };

        this.Load = function (id) {
            ScheduleModelObj.Load(id, function (result) {
                var viewModel = {
                    scheduleid: id,
                    year: result[0].Year,
                    quarter: result[0].Quarter,
                    session: result[0].Session,
                    scheduledayid: result[0].Schedule_day_id,
                    scheduletimeid: result[0].Schedule_time_id,
                    instructorid: result[0].Instructor.Id,
                    courseid: result[0].Course.CourseId,
                    update: function () {
                        self.UpdateSchedule(this);
                    }
                }
                ko.applyBindings(viewModel, document.getElementById("divScheduleEdit"));
            });
        };

        this.GetAll = function (year, quarter) {
            ScheduleModelObj.GetAll(year, quarter, function (scheduleList) {
                scheduleListViewModel.removeAll();

                for (var i = 0; i < scheduleList.length; i++) {
                    scheduleListViewModel.push({
                        scheduleid: scheduleList[i].ScheduleId,
                        year: scheduleList[i].Year,
                        quarter: scheduleList[i].Quarter,
                        session: scheduleList[i].Session,         
                        courseid: scheduleList[i].Course.CourseId,
                        coursedescription: scheduleList[i].Course.Description,
                        coursetitle: scheduleList[i].Course.Title
                    });
                }

                if (initialBind) {
                    ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divScheduleListContent"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            });
        };

        ko.bindingHandlers.DeleteSchedule = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                $(element).click(function () {
                    var id = viewModel.scheduleid;

                    ScheduleModelObj.DeleteSchedule(id, function (result) {
                        if (result != "ok") {
                            alert("Error occurred");
                        } else {
                            scheduleListViewModel.remove(viewModel);
                        }
                    });
                });
            }
        }

       
    }
