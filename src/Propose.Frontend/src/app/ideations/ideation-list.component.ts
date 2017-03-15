import { Ideation } from "./ideation.model";
import { IdeationService } from "./ideation.service";

const template = require("./ideation-list.component.html");
const styles = require("./ideation-list.component.scss");

export class IdeationListComponent extends HTMLElement {
    constructor(
        private _document: Document = document,
        private _ideationService: IdeationService = IdeationService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {
        const ideations: Array<Ideation> = await this._ideationService.get();
        for (var i = 0; i < ideations.length; i++) {
            let el = this._document.createElement(`ce-ideation-item`);
            el.setAttribute("entity", JSON.stringify(ideations[i]));
            this.appendChild(el);
        }    
    }
}

customElements.define("ce-ideation-list", IdeationListComponent);
