import * as ReactDom from "react-dom/client";
import { QuestCreateControl } from "./QuestCreateControl.jsx";

export function renderQuestCreateControl(rootId, context) {
    const root = ReactDom.createRoot(document.getElementById(rootId));

    root.render(
        <QuestCreateControl items={items}/>
    );
}
