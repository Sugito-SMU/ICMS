import { FormArray, FormGroup } from "@angular/forms";
import { ActivityStatusTypes, SYSTEM_CONSTANTS, VALIDATION_PARAMS } from "../system.constants";

export class Helper {
    public static getActivityUpdateValidationMessage(form: FormGroup, stage: string, submit: boolean = false) {

        if (submit && stage == ActivityStatusTypes.PRE.Code && (<FormArray>form.controls.LearningObjectives).controls.filter(c => (<FormGroup>c).controls.IsSelected.value).length != 3) {
            return "Select 3 learning objectives from the list.";
        }

        if (submit) {
            return;
        }

        let check = false;
        switch (stage) {
            case ActivityStatusTypes.POST.Code:
                if ((<FormArray>form.controls.LearningObjectives).controls.filter(lo => (<FormArray>(<FormGroup>lo).controls.LearningObjectiveStageQuestionAnswers).controls.every(loq => (Helper.getWordCount((<FormGroup>loq).controls.Answer.value, !(<FormGroup>lo).controls.IsObjectiveAchieved.value, stage) > VALIDATION_PARAMS.TA_MAX_WC))).length > 0) {
                    check = true;
                }
                break;
            case ActivityStatusTypes.MID.Code:
                if ((<FormArray>form.controls.LearningObjectives).controls.filter(lo => (<FormArray>(<FormGroup>lo).controls.LearningObjectiveStageQuestionAnswers).controls.every(loq => (Helper.getWordCount((<FormGroup>loq).controls.Answer.value, !(<FormGroup>lo).controls.IsMidActivityOnTarget.value, stage) > VALIDATION_PARAMS.TA_MAX_WC))).length > 0) {
                    check = true;
                }
                break;
            case ActivityStatusTypes.PRE.Code:
                if ((<FormArray>form.controls.LearningObjectives).controls.filter(lo => (<FormGroup>lo).controls.IsSelected.value && (<FormArray>(<FormGroup>lo).controls.LearningObjectiveQuestions).controls.find(loq => (Helper.getWordCount((<FormGroup>loq).controls.Answer.value, (<FormGroup>lo).controls.IsSelected.value, stage) > VALIDATION_PARAMS.TA_MAX_WC))).length > 0) {
                    check = true;
                }
                break;
        }

        if (check) {
            return "Learning objective answers should have maximum " + VALIDATION_PARAMS.TA_MAX_WC + " words.";
        }

        if (stage == ActivityStatusTypes.POST.Code &&
            !(<FormArray>form.controls.ActivityQuestionAnswers).controls.every(aq => (Helper.getWordCount((<FormGroup>aq).controls.Answer.value, false, stage) <= VALIDATION_PARAMS.TA_MAX_WC_POSTA))) {
            return "Activity answers should have minimum " + VALIDATION_PARAMS.TA_MIN_WC_POSTA + " words and maximum " + VALIDATION_PARAMS.TA_MAX_WC_POSTA + " words.";
        }

        return null;
    }

    public static getWordCount(text: string, check: boolean, stage: string) {
        let flag = stage == ActivityStatusTypes.PRE.Code ? check : !check;

        if (text && flag) {
            var matches = text.match(SYSTEM_CONSTANTS.WC_REGEX);

            return matches == null ? 0 : matches.length;
        }
        return 0;
    }

    public static getActivityJSONString(formValue: any) {
        var loList = formValue.LearningObjectives;
        var loListLen = loList.length;

        for (let i = 0; i < loListLen; i++) {
            var loQns = loList[i].LearningObjectiveQuestions;
            if (loQns != null) {
                var loQnLen = loQns.length;
                for (let j = 0; j < loQnLen; j++) {
                    if (loQns[j].Answer != null) {
                        loQns[j].Answer = Helper.encodeBase64(loQns[j].Answer);
                    }
                }
            }
            var loQnAns = loList[i].LearningObjectiveStageQuestionAnswers;
            if (loQnAns != null) {
                var loQnAnsLen = loQnAns.length;
                for (let k = 0; k < loQnAnsLen; k++) {
                    if (loQnAns[k].Answer != null) {
                        loQnAns[k].Answer = Helper.encodeBase64(loQnAns[k].Answer);
                    }
                }
            }
        }

        if (formValue.ActivityQuestionAnswers != null) {
            var postQnList = formValue.ActivityQuestionAnswers;
            var postQnListLen = postQnList.length;
            for (let m = 0; m < postQnListLen; m++) {
                if (postQnList[m].Answer != null) {
                    postQnList[m].Answer = Helper.encodeBase64(postQnList[m].Answer);
                }
            }
        }

        var jsonString = JSON.stringify(formValue);

        for (let i = 0; i < loListLen; i++) {
            var loQns = loList[i].LearningObjectiveQuestions;
            if (loQns != null) {
                var loQnLen = loQns.length;
                for (let j = 0; j < loQnLen; j++) {
                    if (loQns[j].Answer != null) {
                        loQns[j].Answer = Helper.decodeBase64(loQns[j].Answer);
                    }
                }
            }
            var loQnAns = loList[i].LearningObjectiveStageQuestionAnswers;
            if (loQnAns != null) {
                var loQnAnsLen = loQnAns.length;
                for (let k = 0; k < loQnAnsLen; k++) {
                    if (loQnAns[k].Answer != null) {
                        loQnAns[k].Answer = Helper.decodeBase64(loQnAns[k].Answer);
                    }
                }
            }
        }

        if (formValue.ActivityQuestionAnswers != null) {
            var postQnList = formValue.ActivityQuestionAnswers;
            var postQnListLen = postQnList.length;
            for (let m = 0; m < postQnListLen; m++) {
                if (postQnList[m].Answer != null) {
                    postQnList[m].Answer = Helper.decodeBase64(postQnList[m].Answer);
                }
            }
        }

        return jsonString;
    }

    public static encodeBase64(e: string) {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var t = "";
        var n, r, i, s, o, u, a;
        var f = 0;
        e = Helper.encodeUTF8(e);
        while (f < e.length) {
            n = e.charCodeAt(f++);
            r = e.charCodeAt(f++);
            i = e.charCodeAt(f++);
            s = n >> 2;
            o = (n & 3) << 4 | r >> 4;
            u = (r & 15) << 2 | i >> 6;
            a = i & 63;
            if (isNaN(r)) {
                u = a = 64
            } else if (isNaN(i)) {
                a = 64
            }
            t = t + keyStr.charAt(s) + keyStr.charAt(o) + keyStr.charAt(u) + keyStr.charAt(a);
        }
        return t
    }

    public static decodeBase64(e: string) {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var t = "";
        var n, r, i;
        var s, o, u, a;
        var f = 0;
        e = e.replace(/[^A-Za-z0-9\+\/\=]/g, "");
        while (f < e.length) {
            s = keyStr.indexOf(e.charAt(f++));
            o = keyStr.indexOf(e.charAt(f++));
            u = keyStr.indexOf(e.charAt(f++));
            a = keyStr.indexOf(e.charAt(f++));
            n = s << 2 | o >> 4;
            r = (o & 15) << 4 | u >> 2;
            i = (u & 3) << 6 | a;
            t = t + String.fromCharCode(n);
            if (u != 64) {
                t = t + String.fromCharCode(r)
            }
            if (a != 64) {
                t = t + String.fromCharCode(i)
            }
        }
        t = Helper.decodeUTF8(t);
        return t;
    }

    public static encodeUTF8(e: string) {
        e = e.replace(/\r\n/g, "\n");
        var t = "";
        for (var n = 0; n < e.length; n++) {
            var r = e.charCodeAt(n);
            if (r < 128) {
                t += String.fromCharCode(r)
            } else if (r > 127 && r < 2048) {
                t +=
                    String.fromCharCode(r >> 6 | 192);
                t +=
                    String.fromCharCode(r & 63 | 128)
            } else {
                t +=
                    String.fromCharCode(r >> 12 | 224);
                t +=
                    String.fromCharCode(r >> 6 & 63 | 128);
                t +=
                    String.fromCharCode(r & 63 | 128)
            }
        }
        return t
    }

    public static decodeUTF8(e: string) {
        var t = "";
        var n = 0;
        var r = 0;
        var c1 = 0;
        var c2 = 0;
        var c3 = 0;
        while (n < e.length) {
            r = e.charCodeAt(n);
            if (r < 128) {
                t += String.fromCharCode(r);
                n++
            } else if (r > 191 && r < 224) {
                c2 = e.charCodeAt(n + 1);
                t += String.fromCharCode(
                    (r & 31) << 6 | c2 & 63);

                n += 2
            } else {
                c2 = e.charCodeAt(n + 1);
                c3 = e.charCodeAt(n + 2);
                t += String.fromCharCode(
                    (r & 15) << 12 | (c2 & 63)
                    << 6 | c3 & 63);
                n += 3
            }
        }
        return t;
    }

}