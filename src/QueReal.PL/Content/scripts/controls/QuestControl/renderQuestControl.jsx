import * as ReactDom from "react-dom/client";
import { QuestControl } from "./QuestControl.jsx";

export function renderQuestControl(rootId, context) {
    const root = ReactDom.createRoot(document.getElementById(rootId));

    root.render(
        <QuestControl items={items}/>
    );
}
