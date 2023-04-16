import { QuestItemCreateRequest } from "./quest-item-create-request";

export class QuestCreateRequest {
    public title: string = null!;

    public questItems: QuestItemCreateRequest[] = null!;
}
