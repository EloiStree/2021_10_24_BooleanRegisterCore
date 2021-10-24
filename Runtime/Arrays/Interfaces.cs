using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooleanRegisterCoreAPI.Interfaces
{
    public interface IBooleanRegisterFull :
        IBooleanRegisterSet, IBooleanRegisterAccess
    { }

    public interface IBooleanDynamicArrayRegister
    {
        void GetSpaceUse(out uint spaceUse, out uint arraySize);
    }
    public interface IBooleanArrayRegister
    {
        void GetMaxSize( out uint arraySize);
    }
    public interface IBooleanRegisterAccess
    {
        void GetValue(in uint index, out bool value);
        void IsIndexValide(in uint index, out bool isValide);
    }
    public interface IBooleanRegisterSet
    {
        void SetValue(in uint index, in bool value);
        void IsIndexValide(in uint index, out bool isValide);
    }

    public interface IBooleanStringRegisterAccess
    {
        void GetValue(in string name, out bool value);
        void GetValue(in string name, out bool containtId, out bool value);
        void Contains(in string name, out bool containsId);
    }
    public interface IBooleanStringRegisterSet
    {
        void Add(in string name, in bool defaultValue);
        void Set(in string name, in bool value);
    }




    //////////////////////////////////////////////


    public interface IAllowBooleanValueRef
    {

        void GetValueRef(in uint index, out IBooleanValueReferenceAccess access);
        void GetValueRef(in uint index, out IBooleanValueReferenceSet access);
    }
    public interface IAllowBooleanStringValueRef
    {

        void GetValueRef(in string index, out IBooleanValueReferenceAccess access);
        void GetValueRef(in string index, out IBooleanValueReferenceSet access);
    }


    public interface IBooleanValueFull :
        IBooleanValueReferenceSourceSet
    , IBooleanRegisterAccess
    , IBooleanValueReferenceAccess
    , IBooleanValueReferenceSet

    {

    }
    public interface IBooleanValueReferenceAccess
    {
        void GetValue(out bool value);
    }
    public interface IBooleanValueReferenceSet
    {
        void SetValue(in bool value);
    }
    public interface IBooleanValueReferenceSourceAccess
    {
        void GetSourceReference(out IBooleanRegisterAccess source);
    }
    public interface IBooleanValueReferenceSourceSet
    {
        void GetSourceReference(out IBooleanRegisterSet source);
    }


    ////////////////////////
    ///

    public interface IBooleanRegisterAcceptFullListeners :
        IBooleanRegisterAcceptListeners
        , IBooleanStringRegisterAcceptListeners
    {

    }
    public interface IBooleanRegisterValueChangeListenerFull :
      IBooleanRegisterValueChangeListener
      , IBooleanStringRegisterValueChangeListener
    {

    }

    public interface IBooleanRegisterAcceptListeners
    {

        void AddListener(IBooleanRegisterValueChangeListener listener);
        void RemoveListener(IBooleanRegisterValueChangeListener listener);
    }
    public interface IBooleanRegisterValueChangeListener
    {

        public void OnValueChange(in uint index, in bool newValue);
    }

    public interface IBooleanStringRegisterAcceptListeners
    {

        void AddListener(IBooleanStringRegisterValueChangeListener listener);
        void RemoveListener(IBooleanStringRegisterValueChangeListener listener);
    }


    public interface IBooleanStringRegisterValueChangeListener
    {

        public void OnValueChange(in string index, in bool newValue);
    }



}
