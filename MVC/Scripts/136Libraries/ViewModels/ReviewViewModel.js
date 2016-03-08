function ReviewViewModel() {
    var courseList = ko.observableArray();
    var reviewListViewModel = ko.observableArray();
    var reviewListObj = new ReviewListModel();
    var self = this;
    var initialBind = true;
    var initialBind1 = true;
    var initialBind2 = true;

    var allQuarters = ["Winter", "Spring", "Summer 1", "Summer 2", "Fall"];
    var allYears = ["2015", "2016", "2017"]; //temporary
    var allSessions = ["A00", "A01", "B00", "B01"];
    var currentQuarter = {
        year: '',
        quarter: ''
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







    this.NewReview = function (id) {
        //var id = "A000002";
         


        var viewModel =
            {
            //TODO: change to @id
            student_id: ko.observable(id),
            schedule_id: ko.observable(''),
            rec_instructor: ko.observable("0"),
            rec_course: ko.observable("0"),
            exams_rep_material: ko.observable("0"),
            instructor_clear: ko.observable("0"),
            expected_grade: ko.observable("F"),
            course_difficult: ko.observable("0"),
            hours_study: ko.observable("0"),
            add: function (data0) {
                
                var Model =
                    {
                        Student: data0.student_id(),
                        Course: data0.schedule_id(),
                        InstructorRating: data0.rec_instructor(),
                        CourseRating: data0.rec_course(),
                        ExamRepMaterialRating: data0.exams_rep_material(),
                        InstructorClearRating: data0.instructor_clear(),
                        ExpectedGrade: data0.expected_grade(),
                        CourseDifficulty: data0.course_difficult(),
                        HoursStudy: data0.hours_study(),
                }
                reviewListObj.Create(Model,
                function (result) {
                    if (result == "ok") {
                        console.log("Create Review successful");
                        self.GetAllStudentReview(id);
                    } else {
                        alert(result);
                    }
                });
            }

        };
        ko.applyBindings(viewModel, document.getElementById("divNewReview"));
    };


    this.AverageProfRating = function (id) {
        reviewListObj.GetAverageProfReview(id, courseList, function (tmp2, reviewAverageList) {
            tmp2.push({
                average: {
                    av_rec_instructor: reviewAverageList.InstructorRating,
                    av_rec_course: reviewAverageList.CourseRating,
                    av_exams_rep_material: reviewAverageList.ExamRepMaterialRating,
                    av_instructor_clear: reviewAverageList.InstructorClearRating,
                    av_expected_grade: reviewAverageList.ExpectedGrade,
                    av_course_difficult: reviewAverageList.CourseDifficulty,
                    av_hours_study: reviewAverageList.HoursStudy,
                }
            });

        });


        if (initialBind1) {
            ko.applyBindings({ viewModel: courseList }, document.getElementById("divYourCourseReview"));
            initialBind1 = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
        }


    };



    this.AverageRating = function (id) {
        console.log("average called" + id);
        reviewListObj.GetAverageReview(id, function (reviewAverageList) {
            courseList.removeAll(); 
            courseList.push({
                cid1: reviewAverageList.Course, 
                av_rec_instructor: (reviewAverageList.InstructorRating),
                av_rec_course: (reviewAverageList.CourseRating),
                av_exams_rep_material: (reviewAverageList.ExamRepMaterialRating),
                av_instructor_clear: (reviewAverageList.InstructorClearRating),
                av_expected_grade: (reviewAverageList.ExpectedGrade),
                av_course_difficult: (reviewAverageList.CourseDifficulty),
                av_hours_study: (reviewAverageList.HoursStudy),           
            });

            if (initialBind1) {
                ko.applyBindings({ viewModel: courseList }, document.getElementById("divYourCourseReview"));
                initialBind1 = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });

    };




    this.GetAll = function () {
        console.log('getall');
        reviewListObj.GetAll(function (reviewListData) {
            reviewListViewModel.removeAll();

            for (var i = 0; i < reviewListData.length; i++) {
                reviewListViewModel.push({
                    student_id: reviewListData[i].Student,
                    schedule_id: reviewListData[i].Course,
                    rec_instructor: reviewListData[i].InstructorRating,
                    rec_course: reviewListData[i].CourseRating,
                    exams_rep_material: reviewListData[i].ExamRepMaterialRating,
                    instructor_clear: reviewListData[i].InstructorClearRating,
                    expected_grade: reviewListData[i].ExpectedGrade,
                    course_difficult: reviewListData[i].CourseDifficulty,
                    hours_study: reviewListData[i].HoursStudy,

                    edit: ko.observable(false),
                    change: function (data) {
                        data.edit(true);
                    },
                    cancel: function (data) {
                        data.edit(false);
                    },
                    submitChange: function (data0) {
                        var data = {
                            Student: data0.student_id,
                            Course: data0.schedule_id,
                            InstructorRating: data0.rec_instructor,
                            CourseRating: data0.rec_course,
                            ExamRepMaterialRating: data0.exams_rep_material,
                            InstructorClearRating: data0.instructor_clear,
                            ExpectedGrade: data0.expected_grade,
                            CourseDifficulty: data0.course_difficult,
                            HoursStudy: data0.hours_study
                        };
                        reviewListObj.edit(data,
                            function (result) {
                                console.log("submit change: " + result);
                                if (result == 'ok') self.GetAll();
                            });

                    },
                    submitChange1: function (data0) {
                        var data = {
                            Student: data0.student_id,
                            Course: data0.schedule_id,
                        };
                        reviewListObj.Delete(data,
                        function (result) {
                            console.log("delete review: " + result);
                            if (result = 'ok') {
                                reviewListViewModel.remove(data0);
                            }
                        });
                    },
                });

                if (initialBind) {
                    ko.applyBindings({ viewModel: reviewListViewModel }, document.getElementById("divYourReview"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            }
        });
    };

    this.GetAllStudentReview = function (id) {
        console.log('getallstudent with id ' + id);
        reviewListObj.GetStudentReview(id, function (reviewListData) {
            reviewListViewModel.removeAll();

            for (var i = 0; i < reviewListData.length; i++) {
                reviewListViewModel.push({
                    student_id: reviewListData[i].Student,
                    schedule_id: reviewListData[i].Course,
                    rec_instructor: reviewListData[i].InstructorRating,
                    rec_course: reviewListData[i].CourseRating,
                    exams_rep_material: reviewListData[i].ExamRepMaterialRating,
                    instructor_clear: reviewListData[i].InstructorClearRating,
                    expected_grade: reviewListData[i].ExpectedGrade,
                    course_difficult: reviewListData[i].CourseDifficulty,
                    hours_study: reviewListData[i].HoursStudy,

                    edit: ko.observable(false),
                    change: function (data) {
                        data.edit(true);
                    },
                    cancel: function (data) {
                        data.edit(false);
                    },
                    submitChange: function (data0) {
                        var data = {
                            Student: data0.student_id,
                            Course: data0.schedule_id,
                            InstructorRating: data0.rec_instructor,
                            CourseRating: data0.rec_course,
                            ExamRepMaterialRating: data0.exams_rep_material,
                            InstructorClearRating: data0.instructor_clear,
                            ExpectedGrade: data0.expected_grade,
                            CourseDifficulty: data0.course_difficult,
                            HoursStudy: data0.hours_study
                        };
                        reviewListObj.edit(data,
                            function (result) {
                                console.log("submit change: " + result);
                               
                                if (result == 'ok') self.GetAllStudentReview(id);
                                else
                                {
                                    alert(result);
                                }
                            });

                    },
                    submitChange1: function (data0) {
                        var data = {
                            Student: data0.student_id,
                            Course: data0.schedule_id,
                        };
                        reviewListObj.Delete(data,
                        function (result) {
                            console.log("delete review: " + result);
                            if (result = 'ok') {
                                reviewListViewModel.remove(data0);
                            }
                        });
                    },
                });

                if (initialBind) {
                    ko.applyBindings({ viewModel: reviewListViewModel }, document.getElementById("divYourReviewSt"));
                    initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
                }
            }
        });
    };

    this.GetCourseReviwList = function (id) {

        reviewListObj.GetCourseReview(id, function (reviewListData) {
            reviewListViewModel.removeAll();

            for (var i = 0; i < reviewListData.length; i++) {
                reviewListViewModel.push({
                    student_id: reviewListData[i].Student,
                    schedule_id: reviewListData[i].Course,
                    rec_instructor: reviewListData[i].InstructorRating,
                    rec_course: reviewListData[i].CourseRating,
                    exams_rep_material: reviewListData[i].ExamRepMaterialRating,
                    instructor_clear: reviewListData[i].InstructorClearRating,
                    expected_grade: reviewListData[i].ExpectedGrade,
                    course_difficult: reviewListData[i].CourseDifficulty,
                    hours_study: reviewListData[i].HoursStudy,

                });
            }
        });

        if (initialBind) {
            ko.applyBindings({ viewModel: reviewListViewModel }, document.getElementById("divYourCourseReview2"));
            initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
        }
    };

    this.GetProfReviwList = function (id) {
        reviewListObj.GetAllProfReview(id, function (reviewListData) {
            reviewListViewModel.removeAll();

            for (var i = 0; i < reviewListData.length; i++) {
                reviewListViewModel.push({
                    student_id: reviewListData[i].Student,
                    schedule_id: reviewListData[i].Course,
                    rec_instructor: reviewListData[i].InstructorRating,
                    rec_course: reviewListData[i].CourseRating,
                    exams_rep_material: reviewListData[i].ExamRepMaterialRating,
                    instructor_clear: reviewListData[i].InstructorClearRating,
                    expected_grade: reviewListData[i].ExpectedGrade,
                    course_difficult: reviewListData[i].CourseDifficulty,
                    hours_study: reviewListData[i].HoursStudy,

                });
            }


        });

        if (initialBind) {
            ko.applyBindings({ viewModel: reviewListViewModel }, document.getElementById("divYourCourseReview2"));
            initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
        }
    };


    this.GetCourseInfo = function (id) {
        self1 = this;
        var viewModel = {
            cid: ko.observable(id),
            update: function (data) {
                self1.AverageRating(data.cid());
                self1.GetCourseReviwList(data.cid());
            }
        }
        if (initialBind2) {
            console.log("updated bind: " + viewModel.cid());
            ko.applyBindings(viewModel, document.getElementById("divYourCourseReviewForm"));
            initialBind2 = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
        }

    };





}