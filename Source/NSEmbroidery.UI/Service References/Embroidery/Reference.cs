﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NSEmbroidery.UI.Embroidery {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GridType", Namespace="http://schemas.datacontract.org/2004/07/NSEmbroidery.Core")]
    public enum GridType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SolidLine = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Points = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Embroidery.IEmbroideryCreatorService")]
    public interface IEmbroideryCreatorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/GetEmbroidery", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/GetEmbroideryResponse")]
        System.IO.Stream GetEmbroidery(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, System.Drawing.Color[] palette, char[] symbols, System.Drawing.Color symbolColor, NSEmbroidery.UI.Embroidery.GridType type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/GetEmbroidery", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/GetEmbroideryResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> GetEmbroideryAsync(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, System.Drawing.Color[] palette, char[] symbols, System.Drawing.Color symbolColor, NSEmbroidery.UI.Embroidery.GridType type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsCount", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsCountResponse")]
        System.Collections.Generic.Dictionary<string, int> PossibleResolutionsCount(System.Drawing.Bitmap image, int cellsCount, int countResolutions);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsCount", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsCountResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> PossibleResolutionsCountAsync(System.Drawing.Bitmap image, int cellsCount, int countResolutions);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutions", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsResponse")]
        System.Collections.Generic.Dictionary<string, int> PossibleResolutions(System.Drawing.Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutions", ReplyAction="http://tempuri.org/IEmbroideryCreatorService/PossibleResolutionsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> PossibleResolutionsAsync(System.Drawing.Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEmbroideryCreatorServiceChannel : NSEmbroidery.UI.Embroidery.IEmbroideryCreatorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EmbroideryCreatorServiceClient : System.ServiceModel.ClientBase<NSEmbroidery.UI.Embroidery.IEmbroideryCreatorService>, NSEmbroidery.UI.Embroidery.IEmbroideryCreatorService {
        
        public EmbroideryCreatorServiceClient() {
        }
        
        public EmbroideryCreatorServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EmbroideryCreatorServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmbroideryCreatorServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EmbroideryCreatorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.IO.Stream GetEmbroidery(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, System.Drawing.Color[] palette, char[] symbols, System.Drawing.Color symbolColor, NSEmbroidery.UI.Embroidery.GridType type) {
            return base.Channel.GetEmbroidery(image, resolutionCoefficient, cellsCount, palette, symbols, symbolColor, type);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> GetEmbroideryAsync(System.Drawing.Bitmap image, int resolutionCoefficient, int cellsCount, System.Drawing.Color[] palette, char[] symbols, System.Drawing.Color symbolColor, NSEmbroidery.UI.Embroidery.GridType type) {
            return base.Channel.GetEmbroideryAsync(image, resolutionCoefficient, cellsCount, palette, symbols, symbolColor, type);
        }
        
        public System.Collections.Generic.Dictionary<string, int> PossibleResolutionsCount(System.Drawing.Bitmap image, int cellsCount, int countResolutions) {
            return base.Channel.PossibleResolutionsCount(image, cellsCount, countResolutions);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> PossibleResolutionsCountAsync(System.Drawing.Bitmap image, int cellsCount, int countResolutions) {
            return base.Channel.PossibleResolutionsCountAsync(image, cellsCount, countResolutions);
        }
        
        public System.Collections.Generic.Dictionary<string, int> PossibleResolutions(System.Drawing.Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient) {
            return base.Channel.PossibleResolutions(image, cellsCount, minCoefficient, maxCoefficient);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> PossibleResolutionsAsync(System.Drawing.Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient) {
            return base.Channel.PossibleResolutionsAsync(image, cellsCount, minCoefficient, maxCoefficient);
        }
    }
}
