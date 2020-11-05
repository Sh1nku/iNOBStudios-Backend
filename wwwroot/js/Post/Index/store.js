var store = new Vuex.Store({
    strict: true,
    state: {
        post: null,
        references: []
    },
    mutations: {
        setModel: (state, value) => {
            state.post = value;
        },
        addReference: (state, value) => {
            state.references.push(value);
        },
        clearReferences: (state) => {
            state.references.splice(0);
        }
    },
    actions: {
        init: (context) => {
            context.commit('setModel', model);
        },
    }
});