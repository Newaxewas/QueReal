import { useEffect, useState } from "react";

export function QuestViewItemControl(props) {
    const [isEditMode, setIsEditMode] = useState(false);
    const [isSendingRequest, setIsSendingRequest] = useState(false);
    const [progress, setProgress] = useState(0);

    useEffect(() => setProgress(props.value.progress), [props.value.progress]);

    if (isEditMode) {
        const onSaveClick = () => {
            setIsSendingRequest(true);

            fetch("/Quest/SetProgress", {
                method: "PUT",
                body: JSON.stringify({
                    questItemId: props.value.id,
                    progress: progress,
                })
            }).then(() => {
                props.onChange(progress);
                setIsEditMode(false);
            }).finally(() => setIsSendingRequest(false));
        };
        const onCancelClick = () => setIsEditMode(false);
        const onProgressChange = (event) => setProgress(event.target.value);

        return (
            <div className="quest-item-details">
                <span className="quest-item-title">{props.value.title}</span>
                <input disabled={isSendingRequest} type="number" min="0" max="100" value={progress} onChange={onProgressChange} />
                <button disabled={isSendingRequest} className="update-button" onClick={onSaveClick}>Save</button>
                <button disabled={isSendingRequest} className="remove-button" onClick={onCancelClick}>Cancel</button>
            </div>
        );
    }
    else {
        const onEditClick = () => setIsEditMode(true);

        return (
            <div className="quest-item-details">
                <span className="quest-item-title">{props.value.title}</span>
                <span>
                    <span>{props.value.progress}%</span>
                    <progress max="100" value={props.value.progress}></progress>
                </span>
                <button className="update-button" onClick={onEditClick}>Edit</button>
            </div>
        );
    }
}