﻿using SelectablePlus.Navigation;
using System.Collections.Generic;
using UnityEngine;

namespace SelectablePlus {

    public class SelectableGroup : MonoBehaviour {

        [SerializeField]
        private SelectableOptionBase firstOption;

        [HideInInspector]
        public List<SelectableOptionBase> options;

        public enum NavigationBuildType { HORIZONTAL, VERTICAL, RAYCAST, UNITY, EXPLICIT }
        public NavigationBuildType navigationType = NavigationBuildType.UNITY;

        /// <summary>
        /// Returns the first option of this group, as set in the inspector.
        /// </summary>
        /// <returns></returns>
        public SelectableOptionBase GetFirstOption() {
            if (firstOption == null) {
                Debug.LogWarning("The SelectableGroup on object " + gameObject.name + " doesn't have a valid first option set! The one with the smallest x or y coordinates, depending on the set navigation build type, will be used as a default!");
                return options[0];
            }

            return firstOption;
        }

        /// <summary>
        /// Builds navigation data for this group using the provided ISelectableNavigationBuilder.
        /// </summary>
        /// <param name="selectableNavigationBuilder">The implementation of ISelectableNavigationBuilder to use</param>
        public void BuildNavigation(ISelectableNavigationBuilder selectableNavigationBuilder) {
            if(selectableNavigationBuilder == null) {
                Debug.LogError("Please provide an ISelectableNavigationBuilder to build the navigation data with!");
                return;
            }

            foreach (SelectableOptionBase option in options) {
                option.ResetNavigation();
            }

            selectableNavigationBuilder.BuildNavigation(this);
        }

    }


}