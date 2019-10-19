(function() {
    $.fn.Koo = function(g) {
        var b = new Object();
        function d(r, t) {
            var p = false;
            var j = "";
            var k = $(r).val();
            for (i = 1; i < t.length; i++) {
                tp_flag = t[i];
                if (t[i].charAt(0) == "!") {
                    if (k == "") {
                        p = false;
                        continue
                    }
                    tp_flag = t[i].substr(1, t[i].length - 1)
                }
                var o = false;
                switch (tp_flag) {
                case "need":
                    if ($.trim(k) != k) {
                        k = $.trim(k);
                        $(r).val(k)
                    }
                    if (k == "") {
                        p = true;
                        j = "\u4e0d\u80fd\u4e3a\u7a7a"
                    }
                    break;
                case "digit":
                    if (!/^[0-9]\d*$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u6570\u5b57"
                    }
                    o = true;
                    break;
                case "chinese":
                    if (k == "" || !/^[\u4e00-\u9fff]*$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u6c49\u5b57"
                    }
                    break;
                case "money":
                    if (!/^\d+(\.\d{1,2})?$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u6b63\u786e\u7684\u91d1\u989d"
                    }
                    o = true;
                    break;
                case "card":
                    if (!/^\d{15}|\d{18}$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u8eab\u4efd\u8bc1\u53f7"
                    }
                    o = true;
                    break;
                case "zip":
                    if (!/^[0-9]\d{5}(?!\d)$/.test(k)) {
                        p = true;
                        j = "\u90ae\u7f16\u9519\u8bef"
                    }
                    o = true;
                    break;
                case "float":
                    if (!/^(-|\+)?\d+(\.\d+)?$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u6570\u5b57"
                    }
                    o = true;
                    break;
                case "tel":
                    if (!/^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$|^\d{11}$|^\d{10}$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u7535\u8bdd\u53f7\u7801"
                    }
                    o = true;
                    break;
                case "mobile":
                    if (!/^(\+?86)?(13[0-9]|15[0-9]|18[0-9])\d{8}$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u624b\u673a\u53f7\u7801"
                    }
                    o = true;
                    break;
                case "char":
                    if (k == "" || !/^[a-z\_\-A-Z]*$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u82f1\u6587\u5b57\u7b26"
                    }
                    o = true;
                    break;
                case "date":
                    if (k != "" && !/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u6b63\u786e\u7684\u65e5\u671f\u683c\u5f0f"
                    }
                    o = true;
                    break;
                case "mail":
                    if (!/^[a-zA-Z0-9]+(\.[_a-zA-Z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+){1,3}$/.test(k)) {
                        p = true;
                        j = "\u8bf7\u8f93\u5165\u90ae\u7bb1\u5730\u5740"
                    }
                    o = true;
                    break;
                default:
                    sinbo_id09323 = tp_flag.substr(1, tp_flag.length - 1).replace("$", "-");
                    switch (tp_flag.charAt(0)) {
                    case "l":
                        var s = parseInt(tp_flag.substr(1));
                        if (k == "" || k.length != s) {
                            p = true;
                            j = "\u957f\u5ea6\u5fc5\u987b\u7b49\u4e8e" + s + "\u4f4d"
                        }
                        break;
                    case "s":
                        var n = parseInt(tp_flag.substr(1));
                        if (k == "" || !/^-?([1-9]\d*\.?\d*|0\.?\d*[1-9]\d*|0?\.?0+|0)$/.test(k) || parseFloat(k) >= n) {
                            p = true;
                            j = "\u5fc5\u987b\u5c0f\u4e8e" + n
                        }
                        break;
                    case "b":
                        var m = parseInt(tp_flag.substr(1));
                        if (k == "" || !/^-?([1-9]\d*\.?\d*|0\.?\d*[1-9]\d*|0?\.?0+|0)$/.test(k) || parseFloat(k) <= m) {
                            p = true;
                            j = "\u5fc5\u987b\u5927\u4e8e" + m
                        }
                        break;
                    case "#":
                        if (document.getElementById(sinbo_id09323).value != k) {
                            p = true;
                            j = "\u4e24\u6b21\u8f93\u5165\u4e0d\u4e00\u81f4"
                        }
                        break;
                    case "%":
                        if (k == "" && document.getElementById(sinbo_id09323).value == "") {
                            p = true;
                            j = "\u81f3\u5c11\u8981\u586b\u51991\u9879"
                        }
                        break;
                    case "_":
                        if (parseInt(k) < parseInt(document.getElementById(sinbo_id09323).value)) {
                            p = true;
                            j = "\u4e0d\u80fd\u5c0f\u4e8e\u5b83"
                        }
                        break;
                    case "*":
                        j = "\u5fc5\u987b\u5927\u4e8e\u524d\u9762\u65f6\u95f4";
                        if (k == "" || document.getElementById(sinbo_id09323).value == "") {
                            p = true
                        }
                        before323 = document.getElementById(sinbo_id09323).value.split("-");
                        curent238 = k.split("-");
                        if (before323.length != curent238.length) {
                            p = true
                        } else {
                            for (c = 0; c < before323.length; c++) {
                                if (parseInt(before323[c]) < parseInt(curent238[c])) {
                                    break
                                } else {
                                    if (parseInt(before323[c]) > parseInt(curent238[c])) {
                                        p = true;
                                        break
                                    }
                                }
                            }
                        }
                        break
                    }
                    break
                }
                if (o) {
                    $(r).css("ime-mode", "disabled")
                }
                if (p) {
                    break
                }
            }
            errID = $(r).attr("id");
            if (errID == undefined) {
                errID = $(r).attr("class")
            }
            errID = "ko" + errID.replace(/[\_|\s\-\|\W]/gi, "");
            cur = $(r).next();
            if (p) {
                if ($(r).attr("title") != undefined && $(r).attr("title") != "") {
                    j = $(r).attr("title")
                }
                if (!$("#" + errID).length) {
                    if (cur.tagName == "SPAN" || (cur[0] != undefined && cur[0].tagName == "SPAN")) {
                        if (b[errID] == undefined) {
                            b[errID] = $(cur).html()
                        }
                        $(cur).html('<span style="color:red;">' + j + "</span>")
                    } else {
                        var q = t[0].substr(t[0].length - 1, 1);
                        if (q == "j") {
                            var l = $('<span style="position:absolute;display:inline-block;z-index:9999" id="' + errID + '" class="kootip">' + j + "</span>");
                            var h = $(r).position();
                            l.css("left", (h.left + 14) + "px");
                            l.css("top", (h.top + $(r).height() + 2) + "px");
                            $(r).after(l)
                        } else {
                            $(r).after('<span id="' + errID + '" class="kootip">' + j + "</span>")
                        }
                    }
                }
            } else {
                if (b[errID] != undefined) {
                    $(cur).html(b[errID])
                } else {
                    if ($("#" + errID)) {
                        $("#" + errID).remove()
                    }
                }
            }
            return ! p
        }
        function f(l) {
            var j = $(l).attr("id");
            if (j == undefined || j.indexOf("-") < 0) {
                j = $(l).attr("class");
                if (j == undefined || j.indexOf("-") < 0) {
                    return null
                }
                var h = $.trim(j).replace(/\s+/g, " ").split(" ");
                for (var k = 0; k < h.length; k++) {
                    if (h[k].indexOf("-") > -1) {
                        j = h[k];
                        break
                    }
                }
            }
            return j.split("-")
        }
        var a = false;
        function e(h) {
            $(h).submit(function() {
                if (a) {
                    return false
                }
                a = true;
                var k = false;
                $("input,select,textarea", this).each(function() {
                    var l = f(this);
                    if (l != null && l.length > 1) {
                        if (!d(this, l)) {
                            k = true
                        }
                    }
                });
                a = false;
                if (k) {
                    return false
                }
                if (g != undefined && typeof(g) == "function") {
                    var j = g();
                    if (typeof(j) == "boolean") {
                        return j
                    }
                }
                return true
            });
            $("input", h).each(function() {
                var j = f(this);
                if (j == null || j.length < 2) {
                    return
                }
                var l = $(this).attr("type");
                if (l == "checkbox" || l == "radio") {
                    if (j[1] == $(this).val()) {
                        $(this).attr("checked", "true")
                    }
                }
                if (j[1] == "date") {
                    var k = $(this).val().split(" ");
                    $(this).val(k[0]);
                    $(this).datepicker();
                    $(this).css("ime-mode", "disabled")
                }
            });
            $("select", h).each(function() {
                var j = f(this);
                if (j != null && j.length > 1) {
                    var k = j[1];
                    $(this).children("option", this).each(function() {
                        if ($(this).attr("value") == k) {
                            $(this).attr("selected", "selected")
                        }
                    });
                    $(this).change(function() {
                        d(this, j)
                    })
                }
            });
            $("optgroup", h).each(function() {
                var j = f(this);
                if (j[1] != undefined && j.length > 1) {
                    var k = j[1];
                    $(this).children("option", this).each(function() {
                        if ($(this).val() == k) {
                            $(this).attr("selected", "selected")
                        }
                    })
                }
            });
            $("input,textarea", h).each(function() {
                var j = f(this);
                if (j != null && j[1].length > 1) {
                    $(this).blur(function() {
                        d(this, j)
                    })
                }
            })
        }
        this.each(function(h, j) {
            e(j)
        })
    }
})(jQuery); (function(a) {
    a.fn.calendar = function(d) {
        d = a.extend({
            initDate: new Date(),
            monthText: ["1\u6708", "2\u6708", "3\u6708", "4\u6708", "5\u6708", "6\u6708", "7\u6708", "8\u6708", "9\u6708", "10\u6708", "11\u6708", "12\u6708"],
            weekText: ["\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d"],
            range: [new Date(1949, 0, 1), new Date(2015, 0, 1)],
            clickEvent: null
        },
        d);
        function e(j, f) {
            var g = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
            var h = (new Date(j, f, 1));
            h.setDate(1);
            if (h.getDate() == 2) {
                h.setDate(0)
            }
            j += 1900;
            return {
                days: f == 1 ? (((j % 4 == 0) && (j % 100 != 0)) || (j % 400 == 0) ? 29 : 28) : g[f],
                firstDay: h.getDay()
            }
        }
        function b(f, h) {
            f.html("");
            var n = e(h.getFullYear(), h.getMonth());
            var p = a('<ul style="list-style:none;width:147px; margin:0px; padding:0px; text-align:center;"></ul>');
            p.append('<li style="width:75px;float:left;"><a href="#" style="display:block;text-decoration:none; color:#2a2a2a;" cal="year" year="' + h.getFullYear() + '" style="display:block;text-decoration:none;color:#2a2a2a;">' + h.getFullYear() + "\u5e74</a></li>").append('<li style="width:70px;float:left;" month="' + h.getMonth() + '"><a href="#" cal="month"  style="display:block; text-decoration:none; color:#2a2a2a;">' + d.monthText[h.getMonth()] + "</a></li>");
            f.append(p);
            var q = a('<ul style="list-style:none; width:147px; margin:0px; padding:0px; text-align:center;"></ul>');
            q.append('<li style="float:left;"><a href="#" cal="preyear" style="display:block; text-decoration:none; color:#2a2a2a;"><<</a></li>').append('<li style="float:left;"><a href="#" cal="preweek" style="display:block; text-decoration:none;width:30px; color:#2a2a2a;"><</a></li>').append('<li style="width:40px;float:left;"><a href="#" cal="today" style="display:block; text-decoration:none; color:#2a2a2a;">\u4eca\u5929</a></li>').append('<li style="float:left;"><a href="#" cal="nextweek"  style="display:block;width:35px; text-decoration:none; color:#2a2a2a;">></a></li>').append('<li style="float:left;"><a href="#" cal="nextyear" style="display:block;text-decoration:none;color:#2a2a2a;">>></a></li>');
            f.append(q);
            var g = a('<ul style="list-style:none; width:147px; margin:0px; padding:0px; text-align:center;"></ul>');
            for (i = 0; i < 7; i++) {
                g.append('<li style="height:auto;float:left;width:21px;height:18px;">' + d.weekText[i] + "</li>")
            }
            f.append(g);
            for (i = 0; i < 6; i++) {
                var r = a('<ul style="list-style:none; width:147px; margin:0px; padding:0px; text-align:center;"></ul>');
                for (var k = 0; k < 7; k++) {
                    var o = 7 * i - n.firstDay + k + 1;
                    var l = o == h.getDate() ? 'style="color:#da2727;text-decoration:underline;font-weight:bold;"': "";
                    if (o > 0 && o <= n.days) {
                        var m = new Date(h.getFullYear(), h.getMonth(), o);
                        if (m >= d.range[0] && m <= d.range[1]) {
                            r.append('<li style="float:left;width:21px;height:18px;text-align:center;"><a href="#" ' + l + ' year="' + h.getFullYear() + '" month="' + h.getMonth() + '" date="' + o + '"  style="display:block; text-decoration:none; color:#2a2a2a;">' + o + "</a></li>")
                        } else {
                            r.append('<li style="color:#dcdcdc;float:left;width:21px;height:18px;">' + o + "</li>")
                        }
                    } else {
                        r.append('<li style="float:left;width:21px;height:18px;">&nbsp;</li>')
                    }
                }
                f.append(r)
            }
            f.find("a").focus(function() {
                this.blur()
            });
            f.find("a").click(function() {
                if (a(this).attr("cal") == "today") {
                    b(f, new Date());
                    if (d.clickEvent != null) {
                        d.clickEvent(new Date())
                    }
                } else {
                    if (a(this).attr("cal") == "preyear") {
                        h.setFullYear(h.getFullYear() - 1);
                        b(f, h)
                    } else {
                        if (a(this).attr("cal") == "nextyear") {
                            h.setFullYear(h.getFullYear() + 1);
                            b(f, h)
                        } else {
                            if (a(this).attr("cal") == "preweek") {
                                h.setMonth(h.getMonth() - 1);
                                b(f, h)
                            } else {
                                if (a(this).attr("cal") == "nextweek") {
                                    h.setMonth(h.getMonth() + 1);
                                    b(f, h)
                                } else {
                                    if (a(this).attr("cal") == "year") {
                                        var u = a('<select style="width:' + (this.clientWidth - 1) + 'px"></select>');
                                        var t = a(this).attr("year");
                                        for (var j = d.range[0].getFullYear(); j <= d.range[1].getFullYear(); j++) {
                                            u.append('<option value="' + j + '">' + j + "</option>")
                                        }
                                        u.change(function() {
                                            h.setFullYear(this.value);
                                            b(f, h)
                                        });
                                        u.val(t);
                                        a(this).replaceWith(u)
                                    } else {
                                        if (a(this).attr("cal") == "month") {
                                            var s = a('<select style="width:' + (this.clientWidth - 2) + 'px"></select>');
                                            t = a(this).parent().attr("month");
                                            for (j = 0; j < 12; j++) {
                                                s.append('<option value="' + j + '">' + d.monthText[j] + "</option>")
                                            }
                                            s.change(function() {
                                                h.setMonth(this.value);
                                                b(f, h)
                                            });
                                            s.val(t);
                                            a(this).replaceWith(s)
                                        } else {
                                            f.find(".calendar_selected").removeAttr("class");
                                            this.className = "calendar_selected";
                                            if (d.clickEvent != null) {
                                                d.clickEvent(new Date(a(this).attr("year"), a(this).attr("month"), a(this).attr("date")))
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return false
            })
        }
        return this.each(function() {
            var g = a(this);
            var f = d.initDate;
            b(g, f)
        })
    }
})(jQuery); (function(a) {
    a.fn.datepicker = function(b) {
        b = a.extend({
            initDate: "",
            monthText: ["1\u6708", "2\u6708", "3\u6708", "4\u6708", "5\u6708", "6\u6708", "7\u6708", "8\u6708", "9\u6708", "10\u6708", "11\u6708", "12\u6708"],
            weekText: ["\u65e5", "\u4e00", "\u4e8c", "\u4e09", "\u56db", "\u4e94", "\u516d"],
            range: [new Date(1949, 0, 1), new Date(2015, 0, 1)],
            splitChar: "-"
        },
        b);
        return this.each(function() {
            a(this).focus(function() {
                if (a("#" + this.id + "_date").length == 0) {
                    var f = a('<div id="' + this.id + '_date" style="font-size:12px; width:152px;z-index:999; height:176px;background:white;border:1px #999 solid;color:#2a2a2a;padding-top:6px; padding-left:6px" class="jabinfo_02"></div>');
                    var e = this;
                    var j = new Date();
                    if (this.value != "") {
                        var h = e.value.split(b.splitChar);
                        if (h.length == 3) {
                            j = new Date(h[0], h[1] - 1, h[2])
                        }
                    }
                    f.calendar({
                        initDate: j,
                        range: b.range,
                        monthText: b.monthText,
                        weekText: b.weekText,
                        clickEvent: function(d) {
                            e.value = d.getFullYear() + b.splitChar + (d.getMonth() + 1) + b.splitChar + d.getDate();
                            f.remove();
                            errID = e.getAttribute("name") + "231314";
                            if (a("#" + errID)) {
                                a("#" + errID).remove()
                            }
                        }
                    });
                    var g = a(this).offset();
                    f.css({
                        position: "absolute",
                        left: a(this).offset().left,
                        top: a(this).offset().top + this.clientHeight
                    });
                    f.mouseover(function() {
                        f.attr("class", "jabinfo_01")
                    });
                    f.mouseout(function() {
                        f.attr("class", "jabinfo_02")
                    });
                    a("body").append(f)
                } else {
                    a("#" + this.id + "_date").remove()
                }
            });
            a(this).blur(function() {
                if (a("#" + this.id + "_date").attr("class") == "jabinfo_02") {
                    a("#" + this.id + "_date").remove()
                }
            })
        })
    }
})(jQuery);