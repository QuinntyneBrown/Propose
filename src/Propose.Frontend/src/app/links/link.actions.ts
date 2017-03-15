import { Link } from "./link.model";

export const linkActions = {
    ADD: "[Link] Add",
    EDIT: "[Link] Edit",
    DELETE: "[Link] Delete",
};

export class LinkEvent extends CustomEvent {
    constructor(eventName:string, link: Link) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { link }
        });
    }
}

export class LinkAdd extends LinkEvent {
    constructor(link: Link) {
        super(linkActions.ADD, link);        
    }
}

export class LinkEdit extends LinkEvent {
    constructor(link: Link) {
        super(linkActions.EDIT, link);
    }
}

export class LinkDelete extends LinkEvent {
    constructor(link: Link) {
        super(linkActions.DELETE, link);
    }
}
