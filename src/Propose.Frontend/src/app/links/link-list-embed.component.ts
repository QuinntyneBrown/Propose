import { Link } from "./link.model";

const template = require("./link-list-embed.component.html");
const styles = require("./link-list-embed.component.scss");

export class LinkListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "links"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.links.length; i++) {
            let el = this._document.createElement(`ce-link-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.links[i]));
            this.appendChild(el);
        }    
    }

    links:Array<Link> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "links":
                this.links = JSON.parse(newValue);
                break;
        }
    }
}

customElements.define("ce-link-list-embed", LinkListEmbedComponent);
