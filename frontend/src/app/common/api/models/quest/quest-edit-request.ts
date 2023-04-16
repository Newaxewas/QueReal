import { QuestItemEditRequest } from "./quest-item-edit-request";

export class QuestEditRequest {

    public id: string = null!;

    public title: string = null!;

    public questItems: QuestItemEditRequest[] = null!;
}
