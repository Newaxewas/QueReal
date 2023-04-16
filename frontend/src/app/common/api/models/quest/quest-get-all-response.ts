import { QuestGetResponse } from "./quest-get-response";

export class QuestGetAllResponse {
    public totalCount: number = null!;

    public quests: QuestGetResponse[] = null!;
}
